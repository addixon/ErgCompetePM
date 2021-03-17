﻿using PM.BO.Enums;
using System;
using System.Collections.Generic;

namespace PM.BO.Interfaces
{
    /// <summary>
    /// Interface for PMService
    /// </summary>
    public interface IPMService
    {
        /// <summary>
        /// Starts default polling on the specified location
        /// </summary>
        /// <param name="location">The location to begin polling</param>
        /// <remarks>
        /// Poll data is returned on time from the EventHandler PollReturned
        /// </remarks>
        void StartPolling(Location location);
        
        /// <summary>
        /// Stops polling on the specified location, or on all locations if null
        /// </summary>
        /// <param name="location">(Optional) The location to stop polling. If null, polling is stopped on all active polling location</param>
        void StopPolling(Location? location = null);
        
        /// <summary>
        /// Discovers all connected Concept2 devices
        /// </summary>
        /// <returns>An IEnumerable of the locations where devices were discovered</returns>
        /// <remarks>
        /// Device data will be returned per device, from the EventHandler DeviceFound. If executed more than once, the potential for lost device events exist from the EventHandler DeviceLost.
        /// </remarks>
        IEnumerable<Location> Discover();

        /// <summary>
        /// Starts device auto discovery, adding and removing devices to the known pool
        /// </summary>
        /// <param name="secondsBetweenDiscovery">The seconds to poll for a change in devices</param>
        /// <remarks>
        /// Device data will be returned per device from the EventHandlers DeviceFound and DeviceLost, based on if they were found or lost.
        /// </remarks>
        void StartAutoDiscovery(int millisecondsBetweenDiscovery = 100);
        
        /// <summary>
        /// Stops device auto discovery
        /// </summary>
        void StopAutoDiscovery();

        /// <summary>
        /// Sets a Just Row workout
        /// </summary>
        /// <param name="location">Location of the device</param>
        /// <param name="splits">Just row with splits if true, with no splits if false</param>
        void SetJustRowWorkout(Location location, bool splits);

        /// <summary>
        /// Sets a fixed workout
        /// </summary>
        /// <remarks>
        /// The values for duration and splits must match the type of the desired workout
        /// </remarks>
        /// <param name="location">Location of the device</param>
        /// <param name="interval">Workout interval</param>
        void SetFixedWorkout(Location location, Interval interval);

        /// <summary>
        /// Sets a variable workout
        /// </summary>
        /// <param name="location">Location of the device</param>
        /// <param name="intervals">Intervals</param>
        void SetVariableWorkout(Location location, IEnumerable<Interval> intervals);

        /// <summary>
        /// Terminate a workout
        /// </summary>
        /// <param name="location">Location of the device</param>
        void TerminateWorkout(Location location);

        /// <summary>
        /// Fires events each time a new device has been found.
        /// </summary>
        event EventHandler? DeviceFound;

        /// <summary>
        /// Fires events each time a device has been lost.
        /// </summary>
        /// <remarks>
        /// If a device is lost and then found, it will be considered a new device
        /// </remarks>
        event EventHandler? DeviceLost;

        /// <summary>
        /// Fires events each time a poll is executed on a device, containing current device data
        /// </summary>
        event EventHandler? PollReturned;
    }
}
