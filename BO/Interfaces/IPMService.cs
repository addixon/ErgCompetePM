using System;
using System.Collections.Generic;

namespace BO.Interfaces
{
    public interface IPMService
    {
        IEnumerable<ushort> Discover();

        void Poll(ushort port);

        void InitializeCommunication();

        event EventHandler? PollReturned;
    }
}
