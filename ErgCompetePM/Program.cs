using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PM.BO;
using PM.BO.Configuration;
using PM.BO.EventArguments;
using PM.BO.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ErgCompetePM
{
    /// <summary>
    /// The program
    /// </summary>
    /// <remarks>
    /// Initiates communication with and polling on all connected PMs
    /// </remarks>
    internal class Program : IHostedService
    {
        /// <summary>
        /// Ensures the display is only refreshed by a single thread at a time
        /// </summary>
        private static readonly object _displayLock;

        /// <summary>
        /// The PM Service
        /// </summary>
        private readonly IPMService _pmService;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<Program> _logger;

        /// <summary>
        /// The configuration for the program
        /// </summary>
        private readonly ProgramConfiguration _configuration;

        /// <summary>
        /// The SignalR hub connection
        /// </summary>
        private HubConnection? _hubConnection;
        
        /// <summary>
        /// Timer to ensure that the hub remains as connected as possible
        /// </summary>
        private Timer? _hubConnectionTimer;

        /// <summary>
        /// Static constructor
        /// </summary>
        static Program()
        {
            _displayLock = new();
        }

        /// <summary>
        /// DI Constructor
        /// </summary>
        /// <param name="pmService">The PM service</param>
        /// <param name="configuration">Program configuration</param>
        /// <param name="logger">The logger</param>
        public Program(IPMService pmService, IOptions<ProgramConfiguration> configuration, ILogger<Program> logger)
        {
            _logger = logger;
            _pmService = pmService;
            _configuration = configuration.Value;
        }

        /// <inheritdoc/>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Program...");

            // Uncomment the below 2 lines to leverage SignalR. An active hub is required to be listening for data
            //_hubConnectionTimer = new Timer(InitiateHub, cancellationToken, 0, 30000);
            //_pmService.PollReturned += RefreshHub;

            _pmService.DeviceFound += StartMonitoringDevice;
            _pmService.DeviceLost += StopMonitoringDevice;
            _pmService.PollReturned += RefreshScreen;
            _pmService.StartAutoDiscovery();

            // Run forever
            do { } while (true);
        }

        /// <inheritdoc/>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _hubConnectionTimer?.Change(Timeout.Infinite, Timeout.Infinite);
            _pmService.StopAutoDiscovery();
        }

        /// <summary>
        /// Initiates the connection with the SignalR hub
        /// </summary>
        /// <param name="state">The program cancellation token</param>
        private void InitiateHub(object? state)
        {
            if (_hubConnection?.State == HubConnectionState.Connected)
            {
                return;
            }

            if (state == null)
            {
                throw new ArgumentNullException(nameof(state), "State was null when initiating hub");
            }

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_configuration.SignalRHubEndpoint)
                .WithAutomaticReconnect()
                .Build();

            try
            {
                Task.Run(async() => await _hubConnection.StartAsync((CancellationToken)state));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hub not starting.");
            }
        }

        /// <summary>
        /// Sends data to the hub
        /// </summary>
        /// <param name="args">The poll data arguments to send to hub</param>
        private void RefreshHub(object? _, EventArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (args is not PollEventArgs pollArgs)
            {
                throw new Exception("Unexpected event arguments.");
            }

            _ = Task.Run(async () =>
              {
                  PerformanceMonitor pm = new()
                  {
                      Data = pollArgs.Data ?? new PMData()
                  };

                  await _hubConnection.InvokeAsync("UpdateErg", pm).ConfigureAwait(false);
              });
        }

        /// <summary>
        /// Starts polling for a found device
        /// </summary>
        /// <param name="args">The device details</param>
        private void StartMonitoringDevice(object? _, EventArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (args is not DeviceEventArgs deviceArgs)
            {
                throw new Exception("Unexpected event arguments.");
            }

            _pmService.StartPolling(deviceArgs.Location);
        }

        /// <summary>
        /// Handles device loss
        /// </summary>
        /// <param name="args">The device details</param>
        private void StopMonitoringDevice(object? _, EventArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (args is not DeviceEventArgs _)
            {
                throw new Exception("Unexpected event arguments.");
            }

            // Do nothing at this time. Polling has been stopped by PMService
        }

        /// <summary>
        /// Refreshes the display with the latest poll data
        /// </summary>
        /// <param name="args">The poll data</param>
        private void RefreshScreen(object? _, EventArgs args)
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
                    throw new Exception("Empty poll data");
                }

                Console.WriteLine("HubConnection: " + _hubConnection?.State);
                Console.WriteLine("Location: " + pollArgs.Location);

                Console.WriteLine("Stroke State: " + pollArgs.Data.StrokeState);
                Console.WriteLine("Calories: " + pollArgs.Data.AccumulatedCalories);
                Console.WriteLine("Horizontal Distance: " + pollArgs.Data.HorizontalDistance);
                Console.WriteLine("Stroke Rate: " + pollArgs.Data.Cadence);
                Console.WriteLine("Drag Factor: " + pollArgs.Data.DragFactor);
            }
        }
    }
}
