using BO.Enums;
using System.Collections.Generic;
using System.Data;

namespace BO.Interfaces
{
    public interface IConnection
    {

        /// <summary>
        /// The Performance Monitor
        /// </summary>
        PerformanceMonitor? PerformanceMonitor { get; }

        /// <summary>
        /// The current state of the connection with the Performance Monitor
        /// </summary>
        ConnectionState State { get; }

        /// <summary>
        /// The number of open attempts for this connection since last healthy
        /// </summary>
        byte OpenAttempts { get; }

        /// <summary>
        /// The number of communication attempts for this connection since last healthy
        /// </summary>
        byte CommunicationAttempts { get; }

        /// <summary>
        /// An accessor to the Performance Monitor's serial number
        /// </summary>
        string? SerialNumber => PerformanceMonitor?.Properties?.SerialNumber;

        /// <summary>
        /// Attempts to open a connection to the performance monitor and establish the Performance Monitor object
        /// </summary>
        /// <returns>True if successful, otherwise false</returns>
        bool Open();

        void RefreshPM3Data(IList<PollInterval> refreshTypes);

        /// <summary>
        /// Closes a connection with the Performance Monitor and destructs the Performance Monitor object
        /// </summary>
        void Close();
    }
}
