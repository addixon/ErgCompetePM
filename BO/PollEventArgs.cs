using BO.Interfaces;
using System;

namespace BO
{
    public class PollEventArgs : EventArgs
    {
        public PMData? Data { get; set; }
        public ushort Port { get; set; }
    }
}
