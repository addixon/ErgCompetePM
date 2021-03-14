using PM.BO;
using PM.BO.EventArguments;
using PM.BO.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace BLL.Hubs
{
    public class ErgHub : Hub<IErgClient>
    {
        /// <summary>
        /// Called from the server to update display based on single erg data
        /// </summary>
        /// <param name="performanceMonitorJson">The performance monitor</param>
        public async Task UpdateErg(PollEventArgs pollEventArgs)
        {
            PerformanceMonitor pm = new PerformanceMonitor
            {
                Data = pollEventArgs?.Data
            };

            // Refresh the screen
            await Clients.All.UpdateDisplay(pm).ConfigureAwait(false);

            return;
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("Connected");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("Disconnected");
            await base.OnDisconnectedAsync(exception);
        }

    }
}
