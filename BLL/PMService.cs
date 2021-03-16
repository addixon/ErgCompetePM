using Microsoft.Extensions.Logging;
using PM.BO.Enums;
using PM.BO.EventArguments;
using PM.BO.Interfaces;
using System;
using System.Collections.Generic;

namespace BLL
{
    /// <summary>
    /// The PM Service initiating & handling access and data to the PM
    /// </summary>
    public class PMService : IPMService
    {
        /// <summary>
        /// The vendor id for Concept2 hardware
        /// </summary>
        private const int VendorId = 0x17A4;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<PMService> _logger;

        /// <summary>
        /// The communicator service
        /// </summary>
        private readonly IPMCommunicator _pmCommunicator;

        /// <summary>
        /// The polling service
        /// </summary>
        private readonly IPMPollingService _pmPollingService;

        /// <inheritdoc />
        public event EventHandler? DeviceFound
        {
            add { _pmCommunicator.DeviceFound += value; }
            remove { _pmCommunicator.DeviceFound -= value; }
        }

        /// <inheritdoc />
        public event EventHandler? DeviceLost
        {
            add { _pmCommunicator.DeviceLost += value; }
            remove { _pmCommunicator.DeviceLost -= value; }
        }


        /// <inheritdoc />
        public event EventHandler? PollReturned
        {
            add { _pmPollingService.PollReturned += value; }
            remove { _pmPollingService.PollReturned -= value; }
        }

        /// <summary>
        /// DI Constructor
        /// </summary>
        /// <param name="pmCommunicator">The communicator service</param>
        /// <param name="pmPollingService">The polling service</param>
        /// <param name="logger">The logger</param>
        public PMService(IPMCommunicator pmCommunicator, IPMPollingService pmPollingService, ILogger<PMService> logger)
        {
            _logger = logger;
            _pmCommunicator = pmCommunicator;
            _pmPollingService = pmPollingService;

            _pmCommunicator.DeviceLost += OnDeviceLost;
        }

        /// <summary>
        /// Deconstructor
        /// </summary>
        ~PMService()
        {
            _pmPollingService.StopPolling();
        }

        /// <inheritdoc />
        public IEnumerable<(int BusNumber, int Address)> Discover()
        {
            return _pmCommunicator.Discover();
        }

        /// <inheritdoc />
        public void StartAutoDiscovery(int secondsBetweenDiscovery = 10)
        {
            _pmCommunicator.StartAutoDiscovery(secondsBetweenDiscovery);
        }

        /// <inheritdoc />
        public void StopAutoDiscovery()
        {
            _pmCommunicator.StopAutoDiscovery();
        }

        /// <inheritdoc />
        public void StartPolling((int BusNumber, int Address) location)
        {
            _logger.LogInformation("Establishing polling intervals.");

            IEnumerable<PollInterval> pollIntervals = new [] 
            { 
                PollInterval.Hz_100, 
                PollInterval.Hz_10, 
                PollInterval.Hz_2, 
                PollInterval.Hz_1 
            };

            _logger.LogInformation("Starting polling on location [{Location}].", location);
            _pmPollingService.StartPolling(location, pollIntervals);
        }

        /// <inheritdoc />
        public void StopPolling((int BusNumber, int Address)? location = null)
        {
            if (location.HasValue)
            {
                _logger.LogInformation("Stopping polling on location [{Location}].", location);
            }
            else
            {
                _logger.LogInformation("Stopping polling on all locations.");
            }

            _pmPollingService.StopPolling(location);
        }

        /// <summary>
        /// Handles cleanup actions when a device is lost
        /// </summary>
        /// <param name="args">Information about the lost device</param>
        private void OnDeviceLost(object? _, EventArgs args)
        {
            if (args is not DeviceEventArgs deviceArgs)
            {
                throw new Exception($"Unexpected arguments in method [{nameof(OnDeviceLost)}]");
            }

            try
            {
                // Close poll if active
                if (_pmPollingService.IsActive(deviceArgs.Location))
                {
                    _pmPollingService.StopPolling(deviceArgs.Location);
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Attempted to stop polling on location [{Location}], but failed.", deviceArgs.Location);
            }
        }
    }
}
