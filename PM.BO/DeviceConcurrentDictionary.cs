using LibUsbDotNet.LibUsb;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace PM.BO
{
    public class DeviceConcurrentDictionary
    {
        private readonly ConcurrentDictionary<(Location Location, string SerialNumber), IUsbDevice> _devices;

        public IEnumerable<string> SerialNumbers => _devices.Keys.Select(key => key.SerialNumber);

        public IEnumerable<Location> Locations => _devices.Keys.Select(key => key.Location);

        public ICollection<IUsbDevice> Values => _devices.Values;

        public DeviceConcurrentDictionary()
        {
            _devices = new();
        }

        public bool TryAdd(Location location, string serialNumber, IUsbDevice device)
        {
            if (string.IsNullOrWhiteSpace(serialNumber))
            {
                throw new ArgumentNullException(nameof(serialNumber), "Serial number must not be null.");
            }

            if (_devices.Keys.Any(key => key.SerialNumber == serialNumber || key.Location == location))
            {
                throw new InvalidOperationException("Key or part of key already exists.");
            }

            return _devices.TryAdd((location, serialNumber), device);
        }

        public bool ContainsKey(Location location)
        {
            return _devices.Keys.Any(key => key.Location == location);
        }

        public bool ContainsKey(string serialNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber))
            {
                throw new ArgumentNullException(nameof(serialNumber), "Serial number must not be null.");
            }

            return _devices.Keys.Any(key => key.SerialNumber == serialNumber);
        }

        public bool TryGetValue(Location location, out IUsbDevice? device)
        {
            (Location, string)? key = GetKeyByLocation(location);
            device = null;

            if (key == null)
            {
                return false;
            }

            return _devices.TryGetValue(key.Value, out device);
        }

        public string? GetSerialNumber(Location location)
        {
            (Location Location, string SerialNumber)? key = GetKeyByLocation(location);

            if (key == null)
            {
                return null;
            }

            return key.Value.SerialNumber;
        }

        public Location? GetLocation(string serialNumber)
        {
            (Location Location, string SerialNumber)? key = GetKeyBySerialNumber(serialNumber);

            if (key == null)
            {
                return null;
            }

            return key.Value.Location;
        }

        public bool TryGetValue(string serialNumber, out IUsbDevice? device)
        {
            (Location, string)? key = GetKeyBySerialNumber(serialNumber);
            device = null;

            if (key == null)
            {
                return false;
            }

            return _devices.TryGetValue(key.Value, out device);
        }

        public bool Remove(Location location, out IUsbDevice? device)
        {
            _ = TryGetValue(location, out device);

            if (device == null)
            {
                device = null;
                return false;
            }

            return true;
        }

        public bool Remove(string serialNumber, out IUsbDevice? device)
        {
            _ = TryGetValue(serialNumber, out device);

            if (device == null)
            {
                device = null;
                return false;
            }

            return true;
        }

        private (Location, string)? GetKeyByLocation(Location location)
        {
            return _devices.Keys.FirstOrDefault(key => key.Location == location);
        }

        private (Location, string)? GetKeyBySerialNumber(string serialNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber))
            {
                throw new ArgumentNullException(nameof(serialNumber), "Serial number must not be null.");
            }

            return _devices.Keys.FirstOrDefault(key => key.SerialNumber == serialNumber);
        }
    }
}
