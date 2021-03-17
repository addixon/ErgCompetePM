using System;

namespace PM.BO.EventArguments
{
    /// <summary>
    /// Data to be sent with device discovery and loss events
    /// </summary>
    public class DeviceEventArgs : EventArgs
    {
        /// <summary>
        /// The location of the device
        /// </summary>
        public Location Location { get; set; }
        
        /// <summary>
        /// The serial number
        /// </summary>
        public string? SerialNumber { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">The location of the device</param>
        public DeviceEventArgs(Location location)
        {
            Location = location;
        }
    }
}
