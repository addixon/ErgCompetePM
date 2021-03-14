using System;

namespace PM.BO.EventArguments
{
    public class DeviceEventArgs : EventArgs
    {
        public (int BusNumber, int Address) Location { get; set; } 
    }
}
