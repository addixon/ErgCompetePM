using PM.BO.Enums;
using System;
using System.Collections.Generic;

namespace PM.BO.Interfaces
{
    public interface IPMPollingService
    {
        event EventHandler? PollReturned;

        void StartPolling((int BusNumber, int Address) location, IEnumerable<PollInterval> pollIntervals);

        void StopPolling((int BusNumber, int Address) location);

        bool IsActive((int BusNumber, int Address) location);
    }
}
