using BLL.Communication;
using PM.BO;
using PM.BO.Commands;
using PM.BO.EventArguments;
using PM.BO.Interfaces;
using LibUsbDotNet.LibUsb;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ErgCompetePM
{
    internal class Program : IHostedService
    {
        private static readonly object _displayLock = new object();

        private readonly IPMService _pmService;
        private readonly ILogger<Program> _logger;
        private Timer _hubConnectTimer;
        private bool _isHubConnected = false;

        private HubConnection? _hubConnection;

        public Program(IPMService pmService, ILogger<Program> logger)
        {
            _logger = logger;
            _pmService = pmService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Program...");

            _hubConnectTimer = new Timer(InitiateHub, cancellationToken, 0, 30000);
            _pmService.DeviceFound += StartMonitoringDevice;
            _pmService.DeviceLost += StopMonitoringDevice;
            _pmService.PollReturned += RefreshScreen;
            _pmService.PollReturned += RefreshHub;
            _pmService.StartAutoDiscovery();

            // Run forever
            do { } while (true);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // Empty
        }

        private void InitiateHub(object? state)
        {
            if (_isHubConnected)
            {
                return;
            }

            if (state == null)
            {
                throw new ArgumentNullException("State was null when initiating hub");
            }

            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://ngrok.ergcompete.com/erg")
                //.WithUrl("http://localhost:49574/erg")
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

        private void StartMonitoringDevice(object? sender, EventArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (args is not DeviceEventArgs deviceArgs)
            {
                throw new Exception("Unexpected event arguments.");
            }

            _pmService.Poll(deviceArgs.Location);
        }

        private void StopMonitoringDevice(object? sender, EventArgs args)
        {

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
                    throw new Exception("Empty poll data");
                }

                Console.WriteLine("HubConnection: " + _hubConnection.State);
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
