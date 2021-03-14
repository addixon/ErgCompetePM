using BLL.Communication;
using BLL.Helpers;
using PM.BO.EventArguments;
using PM.BO.Interfaces;
using LibUsbDotNet;
using LibUsbDotNet.LibUsb;
using Microsoft.Extensions.Logging;
using PM.BO.Comparers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BLL
{
    public class PMCommunicator : IPMCommunicator
    {
        /// <summary>
        /// Locker for pm communication
        /// </summary>
        /// <remarks>
        /// Locks are created for each unique Hub/Address combination
        /// </remarks>
        private static readonly DeviceLocker _deviceLocker = new DeviceLocker();
        private readonly ILogger<PMCommunicator> _logger;
        private static readonly ConcurrentDictionary<(int BusNumber, int Address), DateTime> _lastSends;
        private readonly ConcurrentDictionary<(int BusNumber, int Address), IUsbDevice> _devices;
        private readonly ConcurrentDictionary<(int BustNumber, int Address), (UsbEndpointReader Reader, UsbEndpointWriter writer)> _endpoints; private readonly UsbContext _context;

        private readonly Timer _discoveryTimer;

        public event EventHandler? DeviceFound;
        public event EventHandler? DeviceLost;
        private readonly object _discoveryLock = new object();

        static PMCommunicator ()
        {
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
            _endpoints = new ConcurrentDictionary<(int BusNumber, int Address), (UsbEndpointReader Reader, UsbEndpointWriter writer)>();
            _devices = new ConcurrentDictionary<(int BusNumber, int Address), IUsbDevice>();
            _discoveryTimer = new Timer(InitiateDiscovery, null, Timeout.Infinite, Timeout.Infinite);
        }

        public void StartAutoDiscovery(int secondsBetweenDiscovery = 10)
        {
            _discoveryTimer.Change(0, secondsBetweenDiscovery * 1000);
        }

        public void StopAutoDiscovery()
        {
            _discoveryTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        public IEnumerable<(int BusNumber, int Address)> Discover()
        {
            lock (_discoveryLock)
            {
                UsbDeviceCollection? usbDeviceCollection = _context.List();

                // Filter out all but Concept2 Vendor
                var discoveredDevices = usbDeviceCollection.Where(d => d.VendorId == 0x17A4);

                // Discover disconnected devices
                foreach (UsbDevice detachedDevice in _devices.Values.Except(discoveredDevices, new IUsbDeviceComparer()))
                {
                    (int BusNumber, int Address) location = (detachedDevice.BusNumber, detachedDevice.Address);

                    // If events are enabled, fire one for the device being lost
                    EventArgs args = new DeviceEventArgs
                    {
                        Location = location
                    };

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
                    //foundDevice.SetConfiguration(foundDevice.Configs[0].ConfigurationValue);
                    foundDevice.ClaimInterface(foundDevice.Configs[0].Interfaces[0].Number);

                    UsbEndpointReader reader = foundDevice.OpenEndpointReader(LibUsbDotNet.Main.ReadEndpointID.Ep01);
                    UsbEndpointWriter writer = foundDevice.OpenEndpointWriter(LibUsbDotNet.Main.WriteEndpointID.Ep02);
                    _endpoints[location] = (reader, writer);

                    // If events are enabled, fire one for the device being found
                    EventArgs args = new DeviceEventArgs
                    {
                        Location = location
                    };

                    DeviceFound?.Invoke(this, args);

                    // Add to devices
                    _devices.TryAdd(location, foundDevice);
                }

                return _devices.Values.Select(pm => ((int)((UsbDevice)pm).BusNumber, (int)((UsbDevice)pm).Address));
            }
        }

        public void Send((int BusNumber, int Address) location, ICommandList commands)
        {
            if (!commands.CanSend)
            {
                Exception e = new InvalidOperationException("CommandList was not made ready before Send was called");
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

                (UsbEndpointReader reader, UsbEndpointWriter writer) = _endpoints[location];
                
                // Generate the write buffer and write to PM
                // TODO: Find a way to allow the CommandList to be passed
                byte[] writeBuffer = commands.Buffer.Select(b => (byte)b).ToArray();

                Error writeResult = Error.Other;
                try
                {
                    writeResult = writer.Write(writeBuffer, 100, out int writeBufferSize);
                }
                catch (Exception e)
                {
                    _logger.LogWarning(e, "Exception occurred during writing. Buffer: [{WriteBuffer}]", writeBuffer);
                }
                finally
                {
                    _lastSends[location] = DateTime.UtcNow;
                }

                if (writeResult != Error.Success)
                {
                    _logger.LogWarning("An error occurred while writing. Result: [{WriteResult}])", writeResult);
                    return;
                }

                byte[] readBuffer = new byte[1024];
                Error readResult = Error.Other;
                try 
                { 
                    readResult = reader.Read(readBuffer, 100, out int responseDataSize);
                }
                catch (Exception e)
                {
                    _logger.LogWarning(e, "Exception occurred during reading. Buffer: [{ReadBuffer}]", readBuffer);
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

        private void InitiateDiscovery(object? state)
        {
            _ = Discover();
        }

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