using PM.BO;
using System;
using System.Collections.Generic;

namespace PM.BO.Interfaces
{
    public interface IPMCommunicator
    {
        void Send((int BusNumber, int Address) location, ICommandList commandList);
        IEnumerable<(int BusNumber, int Address)> Discover();
        void StartAutoDiscovery(int secondsBetweenDiscovery = 10);
        void StopAutoDiscovery();

        event EventHandler? DeviceFound;
        event EventHandler? DeviceLost;
    }
}
