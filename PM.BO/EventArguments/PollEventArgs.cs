using System;

namespace PM.BO.EventArguments
{
    /// <summary>
    /// Data to be sent with poll events
    /// </summary>
    public class PollEventArgs : EventArgs
    {
        /// <summary>
        /// The location of the device
        /// </summary>
        public Location? Location { get; set; }
        
        /// <summary>
        /// The data
        /// </summary>
        public PMData? Data { get; set; }
    }
}
