﻿using BLL.Communication;
using BLL.Helpers;
using LibUsbDotNet;
using LibUsbDotNet.LibUsb;
using Microsoft.Extensions.Logging;
using PM.BO.Comparers;
using PM.BO.EventArguments;
using PM.BO.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BLL
{
    /// <summary>
    /// Handles communication with a PM via CSAFE commands
    /// </summary>
    public class PMCommunicator : IPMCommunicator
    {
        /// <summary>
        /// Locker for pm communication
        /// </summary>
        /// <remarks>
        /// Locks are created for each unique Hub/Address combination to control communication flow
        /// </remarks>
        private static readonly DeviceLocker _deviceLocker;

        /// <summary>
        /// Lock to ensure that discovery is not occurring at the same time
        /// </summary>
        private static readonly object _discoveryLock;

        /// <summary>
        /// The vendor id for Concept2 hardware
        /// </summary>
        private const int VendorId = 0x17A4;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<PMCommunicator> _logger;
        
        /// <summary>
        /// Catalog of last sends to the device to ensure the device is not overwhelmed
        /// </summary>
        private static readonly ConcurrentDictionary<(int BusNumber, int Address), DateTime> _lastSends;
        
        /// <summary>
        /// Active devices
        /// </summary>
        private readonly ConcurrentDictionary<(int BusNumber, int Address), IUsbDevice> _devices;
        
        /// <summary>
        /// The usb context
        /// </summary>
        private readonly UsbContext _context;

        /// <summary>
        /// Configurable timer to trigger autodiscovery
        /// </summary>
        private readonly Timer _discoveryTimer;

        /// <inheritdoc />
        public event EventHandler? DeviceFound;

        /// <inheritdoc />
        public event EventHandler? DeviceLost;
        
        /// <summary>
        /// Static constructor
        /// </summary>
        static PMCommunicator ()
        {
            _deviceLocker = new();
            _discoveryLock = new();
            _lastSends = new ConcurrentDictionary<(int BusNumber, int Address), DateTime>();
        }

        /// <summary>
        /// DI Constructor
        /// </summary>
        /// <param name="exceptionActivator">Exception Activator</param>
        /// <param name="logger">Logger</param>
        public PMCommunicator(ILogger<PMCommunicator> logger)
        {
            _logger = logger;
            _context = new UsbContext();
            _devices = new ConcurrentDictionary<(int BusNumber, int Address), IUsbDevice>();
            _discoveryTimer = new Timer(InitiateDiscovery, null, Timeout.Infinite, Timeout.Infinite);
        }

        /// <inheritdoc />
        public void StartAutoDiscovery(int millisecondsBetweenDiscovery = 100)
        {
            _discoveryTimer.Change(0, millisecondsBetweenDiscovery);
        }

        /// <inheritdoc />
        public void StopAutoDiscovery()
        {
            _discoveryTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        /// <inheritdoc />
        public IEnumerable<(int BusNumber, int Address)> Discover()
        {
            lock (_discoveryLock)
            {
                UsbDeviceCollection? usbDeviceCollection = _context.List();

                // Filter out all but Concept2 Vendor
                var discoveredDevices = usbDeviceCollection.Where(d => d.VendorId == VendorId);

                // Discover disconnected devices
                foreach (UsbDevice detachedDevice in _devices.Values.Except(discoveredDevices, new IUsbDeviceComparer()))
                {
                    (int BusNumber, int Address) location = (detachedDevice.BusNumber, detachedDevice.Address);

                    // If events are enabled, fire one for the device being lost
                    EventArgs args = new DeviceEventArgs(location);

                    DeviceLost?.Invoke(this, args);

                    // Clean up device
                    Destroy(detachedDevice);

                    // Remove from devices
                    _devices.Remove(location, out _);
                }

                // Discover new devices
                foreach (UsbDevice foundDevice in discoveredDevices.Except(_devices.Values, new IUsbDeviceComparer()))
                {
                    if (!IsValidDevice(foundDevice))
                    {
                        _logger.LogWarning("Invalid device encountered. Not being added to the device list.");
                    }

                    (int BusNumber, int Address) location = (foundDevice.BusNumber, foundDevice.Address);

                    foundDevice.Open();
                    foundDevice.DetachFromKernel(foundDevice.Configs[0].Interfaces[0].Number);
                    foundDevice.SetConfiguration(foundDevice.Configs[0].ConfigurationValue);
                    foundDevice.ClaimInterface(foundDevice.Configs[0].Interfaces[0].Number);

                    // If events are enabled, fire one for the device being found
                    EventArgs args = new DeviceEventArgs(location)
                    {
                        SerialNumber = foundDevice.Info.SerialNumber
                    };

                    DeviceFound?.Invoke(this, args);

                    // Add to devices
                    _devices.TryAdd(location, foundDevice);
                }

                return _devices.Values.Select(pm => ((int)((UsbDevice)pm).BusNumber, (int)((UsbDevice)pm).Address));
            }
        }

        /// <inheritdoc />
        public void Send((int BusNumber, int Address) location, ICommandList commands)
        {
            if (!commands.CanSend)
            {
                InvalidOperationException e = new("CommandList was not made ready before Send was called");
                _logger.LogError(e, "Send failed.");
                throw e;
            }

            lock (_deviceLocker[location]) 
            {
                const int delayInMilliseconds = 10;

                double? millisecondsSinceLastSend = null;

                if (_lastSends.ContainsKey(location))
                {
                    millisecondsSinceLastSend = (DateTime.UtcNow - _lastSends[location]).TotalMilliseconds;
                }

                if (millisecondsSinceLastSend != null && millisecondsSinceLastSend < delayInMilliseconds)
                {
                    Thread.Sleep(delayInMilliseconds - (int) millisecondsSinceLastSend);
                }

                IUsbDevice device = _devices[location];

                // Generate the write buffer and write to PM
                // TODO: Find a way to allow the CommandList to be passed
                byte[] writeBuffer = commands.Buffer.Select(b => (byte)b).ToArray();

                bool shouldRetry = false;
                int retryCount = 0;
                Error writeResult = Error.Other;
                do
                {
                    UsbEndpointWriter writer = device.OpenEndpointWriter(LibUsbDotNet.Main.WriteEndpointID.Ep02);
                    try
                    {
                        writeResult = writer.Write(writeBuffer, 100, out int writeBufferSize);

                        if (writeResult == Error.Io)
                        {
                            CloseAndOpen(device);
                            shouldRetry = retryCount++ < 2;
                        }
                        else if (writeResult == Error.Success)
                        {
                            shouldRetry = false;
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.LogWarning(e, "Exception occurred during writing. Buffer: [{WriteBuffer}]", writeBuffer);
                    }
                    finally
                    {
                        _lastSends[location] = DateTime.UtcNow;
                    }
                } while (shouldRetry);

                if (writeResult != Error.Success)
                {
                    _logger.LogWarning("An error occurred while writing. Result: [{WriteResult}])", writeResult);
                    return;
                }

                byte[] readBuffer = new byte[1024];
                Error readResult = Error.Other;
                retryCount = 0;
                shouldRetry = false;
                do
                {
                    UsbEndpointReader reader = device.OpenEndpointReader(LibUsbDotNet.Main.ReadEndpointID.Ep01);
                    try
                    {
                        readResult = reader.Read(readBuffer, 100, out int responseDataSize);

                        if (readResult == Error.Io)
                        {
                            CloseAndOpen(device);
                            shouldRetry = retryCount++ < 2;
                        }
                        else if (writeResult == Error.Success)
                        {
                            shouldRetry = false;
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.LogWarning(e, "Exception occurred during reading. Buffer: [{ReadBuffer}]", readBuffer);
                        return;
                    }
                } while (shouldRetry);

                if (readResult != Error.Success)
                {
                    _logger.LogWarning("An error occurred while reading. Result: [{ReadResult}])", readResult);
                    return;
                }

                IResponseReader responseReader = new ResponseReader(readBuffer.Select(b => (uint) b));
                bool responseReaderSuccess = commands.Read(responseReader);

                if (!responseReaderSuccess)
                {
                    _logger.LogWarning("An error occurred while consuming the read buffer. Result: [{ResponseReadResult}])", responseReaderSuccess);
                    return;
                }
            }
        }

        /// <summary>
        /// Close and open the device
        /// </summary>
        /// <param name="device">The device</param>
        private static void CloseAndOpen(IUsbDevice device)
        {
            if (device.IsOpen)
            {
                device.Close();
            }

            device.Open();
            ((UsbDevice)device).DetachFromKernel(device.Configs[0].Interfaces[0].Number);
            device.SetConfiguration(device.Configs[0].ConfigurationValue);
            device.ClaimInterface(device.Configs[0].Interfaces[0].Number);
        }

        /// <summary>
        /// Cleans up a device, releasing interfaces and closing connection
        /// </summary>
        /// <param name="device">The device to close</param>
        private void Destroy(IUsbDevice device)
        {
            try
            {
                // Attempt to clean up device
                device.ReleaseInterface(device.Configs[0].Interfaces[0].Number);
                device.Close();
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Attempted to clean up device, but failed.");
            }
        }

        /// <summary>
        /// Initiates discovery
        /// </summary>
        private void InitiateDiscovery(object? _)
        {
            _ = Discover();
        }

        /// <summary>
        /// Checks if the device is valid and can be communicated with
        /// </summary>
        /// <param name="device">The device</param>
        /// <returns>True if valid, false otherwise</returns>
        private bool IsValidDevice(IUsbDevice device)
        {
            if (device == null)
            {
                _logger.LogWarning("Device was found, but discovered to be null");
                return false;
            }

            if (!device.Configs.Any())
            {
                _logger.LogWarning("Device was found, but had no configuration.");
                return false;
            }

            if (!device.Configs[0].Interfaces.Any())
            {
                _logger.LogWarning("Device was found, but had no interfaces.");
                return false;
            }

            if (device.Configs[0].Interfaces[0].Endpoints.Count != 2)
            {
                _logger.LogWarning("Device was found, but did not have 2 endpoints.");
                return false;
            }

            return true;
        }
    }
}