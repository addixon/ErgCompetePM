using PM.BO.Enums;
using System;
using System.Collections.Generic;

namespace PM.BO.Interfaces
{
    /// <summary>
    /// The interface for the polling service
    /// </summary>
    public interface IPMPollingService
    {
        /// <summary>
        /// Starts default polling on the specified location
        /// </summary>
        /// <param name="location">The location to begin polling</param>
        /// <remarks>
        /// Poll data is returned on time from the EventHandler PollReturned. 
        /// Polling only occurs when the WorkoutState is in that of an active workout
        /// </remarks>
        void StartPolling((int BusNumber, int Address) location, IEnumerable<PollInterval> pollIntervals);

        /// <summary>
        /// Stops polling on the specified location, or on all locations if null
        /// </summary>
        /// <param name="location">(Optional) The location to stop polling. If null, polling is stopped on all active polling location</param>
        void StopPolling((int BusNumber, int Address)? location = null);

        /// <summary>
        /// Checks if a device at a specified location has an active poll
        /// </summary>
        /// <param name="location">The location of the device</param>
        /// <returns>True if there is an active poll, false otherwise</returns>
        bool IsActive((int BusNumber, int Address) location);

        /// <summary>
        /// Fires events each time a poll is executed on a device, containing current device data
        /// </summary>
        event EventHandler? PollReturned;
    }
}
