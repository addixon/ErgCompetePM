using System.Collections.Generic;

namespace BO.Interfaces
{
    public interface IPMCommunicator
    {
        void InitializeCSafe();

        void InitializeDDI();

        /// <summary>
        /// Gets all ports where Performance Monitors are discovered
        /// </summary>
        /// <returns>An enumeration of the ports where Performance Monitors are located on</returns>
        IEnumerable<ushort> DiscoverPorts();

        /// <summary>
        /// Gets the serial number of the performance monitor at the provided location
        /// </summary>
        /// <param name="port">The port to get the Serial Number from</param>
        /// <returns>The serial number of the Performance Monitor at the specified ports</returns>
        string GetSerialNumber(ushort port);

        /// <summary>
        /// Sends the command list to the PM at the specified port
        /// </summary>
        /// <param name="port">The port</param>
        /// <param name="commandList">The commands to send</param>
        void Send(ushort port, ICommandList commandList);
    }
}
