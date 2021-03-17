using LibUsbDotNet;
using LibUsbDotNet.Info;
using LibUsbDotNet.LibUsb;
using LibUsbDotNet.Main;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace PM.BO
{
    public class PMDevice : IUsbDevice, IComparable<PMDevice>, IEqualityComparer<PMDevice>
    {
        private IUsbDevice _device;

        public Guid Id { get; }

        public DateTime DateCreated { get; }

        public DateTime LastSuccessfulCommunication { get; set; }

        public int CommunicationErrors { get; set; }

        public string SerialNumber => _device.Info.SerialNumber;

        public Location Location { get; }

        public PMDevice(IUsbDevice device)
        {
            Id = Guid.NewGuid();
            DateCreated = DateTime.UtcNow;

            _device = device ?? throw new ArgumentNullException(nameof(device), "Device must not be null when creating a PM.");

            if (device is not UsbDevice usbDevice)
            {
                throw new InvalidOperationException("Device must be a UsbDevice to be a PMDevice.");
            }

            Location = new (usbDevice.BusNumber, usbDevice.Address);
        }

        public void RefreshDevice(IUsbDevice device)
        {
            _device = device;
        }

        public bool Equals(PMDevice? device)
        {
            if (device == null)
            {
                return false;
            }

            if (device.Location == Location)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Compares two PMDevices
        /// </summary>
        /// <param name="device"></param>
        /// <returns>0 if equal, 1 if exists but serial number or location have changed, -1 if does not exist</returns>
        public int CompareTo(PMDevice? device)
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device), "Cannot compare to null device.");
            }

            if (device.Location == Location)
            {
                return 0;
            } 

            return -1;
        }

        #region IUsbDevice Implementation
        public DeviceHandle DeviceHandle => _device.DeviceHandle;

        public ReadOnlyCollection<UsbConfigInfo> Configs => _device.Configs;

        public UsbDeviceInfo Info => _device.Info;

        public bool IsOpen => _device.IsOpen;

        public int Configuration => _device.Configuration;

        public ushort VendorId => _device.VendorId;

        public ushort ProductId => _device.ProductId;

        public void Close()
        {
            _device.Close();
        }

        public bool GetDescriptor(byte descriptorType, byte index, short langId, IntPtr buffer, int bufferLength, out int transferLength)
        {
            return _device.GetDescriptor(descriptorType, index, langId, buffer, bufferLength, out transferLength);
        }

        public bool GetDescriptor(byte descriptorType, byte index, short langId, object buffer, int bufferLength, out int transferLength)
        {
            return _device.GetDescriptor(descriptorType, index, langId, buffer, bufferLength, out transferLength);
        }

        public bool GetLangIDs(out short[] langIDs)
        {
            return _device.GetLangIDs(out langIDs);
        }

        public bool GetString(out string stringData, short langId, byte stringIndex)
        {
            return _device.GetString(out stringData, langId, stringIndex);
        }

        public string GetStringDescriptor(byte descriptorIndex, bool failSilently = false)
        {
            return _device.GetStringDescriptor(descriptorIndex, failSilently);
        }

        public bool TryGetConfigDescriptor(byte configIndex, out UsbConfigInfo descriptor)
        {
            return _device.TryGetConfigDescriptor(configIndex, out descriptor);
        }

        public bool SetAltInterface(int alternateID)
        {
            return _device.SetAltInterface(alternateID);
        }

        public bool GetAltInterface(out int alternateID)
        {
            return _device.GetAltInterface(out alternateID);
        }

        public void Open()
        {
            _device.Open();
        }

        public bool TryOpen()
        {
            return _device.TryOpen();
        }

        public UsbEndpointReader OpenEndpointReader(ReadEndpointID readEndpointID, int readBufferSize)
        {
            return _device.OpenEndpointReader(readEndpointID, readBufferSize);
        }

        public UsbEndpointReader OpenEndpointReader(ReadEndpointID readEndpointID, int readBufferSize, EndpointType endpointType)
        {
            return _device.OpenEndpointReader(readEndpointID, readBufferSize, endpointType);
        }

        public UsbEndpointReader OpenEndpointReader(ReadEndpointID readEndpointID)
        {
            return _device.OpenEndpointReader(readEndpointID);
        }

        public UsbEndpointWriter OpenEndpointWriter(WriteEndpointID writeEndpointID)
        {
            return _device.OpenEndpointWriter(writeEndpointID);
        }

        public UsbEndpointWriter OpenEndpointWriter(WriteEndpointID writeEndpointID, EndpointType endpointType)
        {
            return _device.OpenEndpointWriter(writeEndpointID, endpointType);
        }

        public void SetConfiguration(int config)
        {
            _device.SetConfiguration(config);
        }

        public void GetAltInterfaceSetting(byte interfaceID, out byte selectedAltInterfaceID)
        {
            _device.GetAltInterfaceSetting(interfaceID, out selectedAltInterfaceID);
        }

        public bool ClaimInterface(int interfaceID)
        {
            return _device.ClaimInterface(interfaceID);
        }

        public bool ReleaseInterface(int interfaceID)
        {
            return _device.ReleaseInterface(interfaceID);
        }

        public void ResetDevice()
        {
            _device.ResetDevice();
        }

        public int ControlTransfer(UsbSetupPacket setupPacket)
        {
            return _device.ControlTransfer(setupPacket);
        }

        public int ControlTransfer(UsbSetupPacket setupPacket, byte[] buffer, int offset, int length)
        {
            return _device.ControlTransfer(setupPacket, buffer, offset, length);
        }

        public IUsbDevice Clone()
        {
            return _device.Clone();
        }

        public void Dispose()
        {
            _device.Dispose();
        }

        public bool Equals(PMDevice? x, PMDevice? y)
        {
            return x?.Location == y?.Location;
        }

        public int GetHashCode([DisallowNull] PMDevice obj)
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
