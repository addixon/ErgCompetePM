using BO.Enums;
using System;
using System.Collections.Generic;

namespace BO.Interfaces
{
    public interface IPMPollingService
    {
        event EventHandler? PollReturned;

        void StartPolling(ushort port, IEnumerable<PollInterval> pollIntervals);

        void StopPolling(ushort port);

        void OnPollReturned(EventArgs args);
    }
}
