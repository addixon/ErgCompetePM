﻿using System;

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
        public (int BusNumber, int Address) Location { get; set; }
        
        /// <summary>
        /// The serial number
        /// </summary>
        public string? SerialNumber { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">The location of the device</param>
        public DeviceEventArgs((int BusNumber, int Address) location)
        {
            Location = location;
        }
    }
}
