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
        /// DI Constructor
        /// </summary>
        /// <param name="logger">The logger</param>
        public ErgHub(ILogger<ErgHub> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Called to initiate an send to the hub
        /// </summary>
        /// <param name="performanceMonitor">The performance monitor</param>
        public async Task SendStatisticsToHub(IErg performanceMonitor)
        {
            if (performanceMonitor == null)
            {
                _logger.LogWarning("When requested to send statistics to hub, the performance monitor was null. Returning without sending.");
                return;
            }

            // Send the data
            await Clients.All.ErgToHub(performanceMonitor).ConfigureAwait(false);

            return;
        }

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
