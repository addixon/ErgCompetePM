using BO.Enums;
using BO.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class PMService : IPMService
    {
        private readonly ILogger<PMService> _logger;
        private readonly IPMCommunicator _pmCommunicator;
        private readonly IPMPollingService _pmPollingService;

        public event EventHandler? PollReturned
        {
            add { _pmPollingService.PollReturned += value; }
            remove { _pmPollingService.PollReturned -= value; }
        }

        public PMService(IPMCommunicator pmCommunicator, IPMPollingService pmPollingService, ILogger<PMService> logger)
        {
            _logger = logger;
            _pmCommunicator = pmCommunicator;
            _pmPollingService = pmPollingService;
        }

        public IEnumerable<ushort> Discover()
        {
            _logger.LogInformation("Initializing DDI Protocol.");

            // Ensure the DDI protocol is initiated
            _pmCommunicator.InitializeDDI();

            _logger.LogInformation("Discovering devices.");

            // Return the ports where PMs are discovered
            IEnumerable<ushort> ports = _pmCommunicator.DiscoverPorts();

            _logger.LogInformation("[{PortCount}] ports found.", ports.Count());

            return ports;
        }

        public void InitializeCommunication()
        {
            _logger.LogInformation("Initializing CSAFE Protocol.");

            // Ensure the CSAFE protocol is initiated
            _pmCommunicator.InitializeCSafe();
        }

        public void Poll(ushort port)
        {
            _logger.LogInformation("Establishing polling intervals.");

            IEnumerable<PollInterval> pollIntervals = new [] 
            { 
                PollInterval.Hz_100, 
                PollInterval.Hz_10, 
                PollInterval.Hz_2, 
                PollInterval.Hz_1 
            };

            _logger.LogInformation("Starting polling on port [{Port}].", port);

            _pmPollingService.StartPolling(port, pollIntervals);
        }
    }
}
