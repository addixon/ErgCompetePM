using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using PM.BO.Interfaces;
using System;
using System.Threading.Tasks;

namespace BLL.Hubs
{
    /// <summary>
    /// SignalR Hub Service
    /// </summary>
    public class ErgHub : Hub<IErgClient>
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<ErgHub> _logger;

        /// <summary>
        /// The PM service
        /// </summary>
        private readonly IPMService _pmService;

        /// <summary>
        /// DI Constructor
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="pmService">The pm service</param>
        public ErgHub(IPMService pmService, ILogger<ErgHub> logger)
        {
            _pmService = pmService;
            _logger = logger;
        }

        #region Hub To Erg
        // TODO: Once an erg has been established with the server (using an ititiating hub call, serial number shouldn't be needed for each communication because the channel should be open and unique
        public async Task DefineWorkout(string serialNumber, IWorkout workout)
        {
            if (string.IsNullOrWhiteSpace(serialNumber))
            {
                _logger.LogWarning("When requested to send a workout to the Erg, the serial number was null. Returning without sending.");
                return;
            }

            if (workout == null)
            {
                _logger.LogWarning("When requested to send a workout to the Erg, the workout was null. Returning without sending.");
                return;
            }

            // Send the workout
            _pmService.SetWorkout(serialNumber, workout);
        }
        #endregion

        #region Erg To Hub
        /// <summary>
        /// Called to initiate an send to the hub
        /// </summary>
        /// <param name="performanceMonitor">The performance monitor</param>
        public async Task BroadcastWorkoutData(IErg performanceMonitor)
        {
            if (performanceMonitor == null)
            {
                _logger.LogWarning("When requested to send statistics to hub, the performance monitor was null. Returning without sending.");
                return;
            }

            // Send the data
            await Clients.All.BroadcastWorkoutStatistics(performanceMonitor).ConfigureAwait(false);

            return;
        }

        // TODO: This may not be required if SignalR has built in functionality to achieve this. However, it must be considered that a SignalR heartbeat could differ from a communication heartbeat with the PM
        public async Task BroadcastHeartbeat(string serialNumber)
        {
            // Sends the heartbeat to hub to allow connected/listening devices to be shown

            // Send the data
            await Clients.All.BroadcastHeartbeat(serialNumber).ConfigureAwait(false);

            return;
        }

        /// <summary>
        /// Conveys lost & found and user tag in & out events to the hub 
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        // TODO: This could take place of Heartbeat if heartbeat was a specific type of status
        public async Task BroadcastStatus(string serialNumber)
        {
            // Send the data
            await Clients.All.BroadcastStatus(serialNumber).ConfigureAwait(false);

            return;
        }
        #endregion

        /// <inheritdoc/>
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("Connected");
            await base.OnConnectedAsync();
        }

        /// <inheritdoc/>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("Disconnected");
            await base.OnDisconnectedAsync(exception);
        }

    }
}
