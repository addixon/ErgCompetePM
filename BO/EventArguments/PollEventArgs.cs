using System;

namespace PM.BO.EventArguments
{
    public class PollEventArgs : EventArgs
    {
        public PMData? Data { get; set; }
        public (int BusNumber, int Address)? Location { get; set; }
    }
}
