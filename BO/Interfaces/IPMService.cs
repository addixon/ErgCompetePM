using LibUsbDotNet.LibUsb;
using PM.BO;
using System;
using System.Collections.Generic;

namespace PM.BO.Interfaces
{
    public interface IPMService
    {
        void Poll((int BusNumber, int Address) location);
        IEnumerable<(int BusNumber, int Address)> Discover();
        void StartAutoDiscovery(int secondsBetweenDiscovery = 10);
        void StopAutoDiscovery();
        
        event EventHandler? DeviceFound;
        event EventHandler? DeviceLost;
        event EventHandler? PollReturned;

        void QuickTest();
    }
}
