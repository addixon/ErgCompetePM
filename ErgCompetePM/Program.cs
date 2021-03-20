using BLL.Hubs;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PM.BO;
using PM.BO.Configuration;
using PM.BO.EventArguments;
using PM.BO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly Timer? _hubConnectionTimer;

        private static readonly IList<string> _activeDevices;

        /// <summary>
        /// Static constructor
        /// </summary>
        static Program()
        {
            _displayLock = new();
            _activeDevices = new List<string>();
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
            // Uncomment the below 2 lines to leverage SignalR. An active hub is required to be listening for data
            //_hubConnectionTimer = new Timer(InitiateHub, cancellationToken, 0, 30000);
            //_pmService.PollReturned += RefreshHub;

            _pmService.DeviceFound += StartMonitoringDevice;
            _pmService.DeviceLost += StopMonitoringDevice;
            _pmService.PollReturned += RefreshScreen;

            Console.WriteLine("Starting to listen for devices.");
            _pmService.StartAutoDiscovery();

            Thread.Sleep(5000);

            // Establish a Just Row workout without splits and set the screen
            //_pmService.SetJustRowWorkout(_activeDevices.First(), false);

            // Establish a fixed interval workout for distance with splits
            //Interval interval = new()
            //{
            //    Duration = 2000,
            //    WorkoutType = PM.BO.Enums.WorkoutType.FixedDistanceWithSplits,
            //    Splits = 400
            //};
            //_pmService.SetFixedWorkout(_activeDevices.First(), interval);
            
            // Establish a variable interval workout for distance, time, distance, and time with defined rest intervals
            //Interval[] intervals = new Interval[]
            //{
            //    new()
            //    {
            //        Duration = 500,
            //        IntervalType = PM.BO.Enums.IntervalType.Distance,
            //        SecondsOfRest = 60,
            //        TargetPace = 100
            //    },
            //    new()
            //    {
            //        Duration = 180,
            //        IntervalType = PM.BO.Enums.IntervalType.Time,
            //        SecondsOfRest = 0,
            //        TargetPace = 100
            //    },
            //    new()
            //    {
            //        Duration = 1000,
            //        IntervalType = PM.BO.Enums.IntervalType.Distance,
            //        SecondsOfRest = 0,
            //        TargetPace = 100
            //    },
            //    new()
            //    {
            //        Duration = 600,
            //        IntervalType = PM.BO.Enums.IntervalType.Time,
            //        SecondsOfRest = 120,
            //        TargetPace = 100
            //    }
            //};
            //_pmService.SetVariableWorkout(_activeDevices.First(), intervals);

            // Establish a variable interval workout for distance and time with undefined rests
            // NOTE: Always use variable workout intervals for any workout that includes undefined rests
            //Interval[] intervals1 = new Interval[]
            //{
            //    new()
            //    {
            //        Duration = 100,
            //        IntervalType = PM.BO.Enums.IntervalType.DistanceUndefinedRest,
            //        TargetPace = 130
            //    },
            //    new()
            //    {
            //        Duration = 120,
            //        IntervalType = PM.BO.Enums.IntervalType.TimeUndefinedRest,
            //        TargetPace = 130
            //    }
            //};
            //_pmService.SetVariableWorkout(_activeDevices.First(), intervals1);

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
                  PM.BO.PM pm = new()
                  {
                      Data = pollArgs.Data ?? new PMData()
                  };

                  await _hubConnection.InvokeAsync(nameof(ErgHub.SendStatisticsToHub), pm).ConfigureAwait(false);
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

            if (deviceArgs.SerialNumber == null)
            {
                _logger.LogCritical("Serial number was null when StartMonitoring was acted on.");
                return;
            }

            Console.WriteLine("Device found!");

            _activeDevices.Add(deviceArgs.SerialNumber);
            //_pmService.StartPolling(deviceArgs.SerialNumber);
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

            if (args is not DeviceEventArgs deviceArgs)
            {
                throw new Exception("Unexpected event arguments.");
            }

            if (deviceArgs?.SerialNumber == null)
            {
                _logger.LogCritical("Serial number was null when StartMonitoring was acted on.");
                return;
            }

            Console.WriteLine("Device lost!");

            _activeDevices.Remove(deviceArgs.SerialNumber);

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
                Console.WriteLine("Serial Number: " + pollArgs.SerialNumber);

                Console.WriteLine("Stroke State: " + pollArgs.Data.StrokeState);
                Console.WriteLine("Calories: " + pollArgs.Data.AccumulatedCalories);
                Console.WriteLine("Horizontal Distance: " + pollArgs.Data.HorizontalDistance);
                Console.WriteLine("Stroke Rate: " + pollArgs.Data.Cadence);
                Console.WriteLine("Drag Factor: " + pollArgs.Data.DragFactor);
            }
        }
    }
}
