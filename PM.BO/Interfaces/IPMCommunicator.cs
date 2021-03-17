using System;
using System.Collections.Generic;

namespace PM.BO.Interfaces
{
    /// <summary>
    /// Interface for communication with the PM
    /// </summary>
    public interface IPMCommunicator
    {
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
        /// Discovers all connected Concept2 devices
        /// </summary>
        /// <returns>An IEnumerable of the locations where devices were discovered</returns>
        /// <remarks>
        /// Device data will be returned per device, from the EventHandler DeviceFound. If executed more than once, the potential for lost device events exist from the EventHandler DeviceLost.
        /// </remarks>
        IEnumerable<Location> Discover();

        /// <summary>
        /// Sends commands to the PM
        /// </summary>
        /// <remarks>
        /// Any data retrieved from the PM is available on the command via Value
        /// </remarks>
        /// <param name="location">The location of the PM</param>
        /// <param name="commandList">The commands</param>
        void Send(Location location, ICommandList commandList);

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
    }
}
