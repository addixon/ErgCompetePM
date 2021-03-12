using BO;
using BO.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ErgCompetePM
{
    internal class Program : IHostedService
    {
        private static object _displayLock = new object();

        private readonly IPMService _pmService;
        private readonly ILogger<Program> _logger;

        private IList<Task> _performanceMonitors;

        private HubConnection _hubConnection;

        public Program(IPMService pmService, ILogger<Program> logger)
        {
            _logger = logger;
            _pmService = pmService;

            _performanceMonitors = new List<Task>();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Program...");

            IList<ushort> ports = _pmService.Discover().ToList();

            if (ports.Any()) 
            {
                _logger.LogInformation("Intializing CSafe Protocol");
                _pmService.InitializeCommunication();
            }

            foreach (ushort port in ports)
            {
                _logger.LogInformation("Creating poll task for port [{Port}]", port);
                _performanceMonitors.Add(Task.Run(() => _pmService.Poll(port), cancellationToken));
                _pmService.PollReturned += RefreshScreen;
                _pmService.PollReturned += RefreshHub;
            }

            await InitiateHub(cancellationToken);

            // Run forever
            do { } while (true);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // Empty
        }

        private async Task InitiateHub(CancellationToken cancellationToken)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://ngrok.ergcompete.com/erg")
                .WithAutomaticReconnect()
                .Build();

            try
            {
                await _hubConnection.StartAsync(cancellationToken);
                Console.WriteLine("Hub Connection started");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
            }
        }

        private void RefreshHub(object? sender, EventArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (args is not PollEventArgs pollArgs)
            {
                throw new Exception("Unexpected event arguments.");
            }

            Task.Run(async () =>
            {
                PerformanceMonitor pm = new PerformanceMonitor
                {
                    Data = pollArgs.Data ?? new PMData()
                };
                await _hubConnection.InvokeAsync("UpdateErg", pm).ConfigureAwait(false);
            });
        }

        private void RefreshScreen(object? sender, EventArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (args is not PollEventArgs pollArgs)
            {
                throw new Exception("Unexpected event arguments.");
            }

            lock (_displayLock) 
            { 
                Console.Clear();

                if (pollArgs.Data == null)
                {
                    Console.WriteLine("Data is empty");
                    return;
                }

                Console.WriteLine("Port: " + pollArgs.Port);

                Console.WriteLine("Stroke State: " + pollArgs.Data.StrokeState);
                Console.WriteLine("Calories: " + pollArgs.Data.AccumulatedCalories);
                Console.WriteLine("Horizontal Distance: " + pollArgs.Data.HorizontalDistance);
                Console.WriteLine("Stroke Rate: " + pollArgs.Data.Cadence);
                Console.WriteLine("Drag Factor: " + pollArgs.Data.DragFactor);
            }
        }
    }
}
