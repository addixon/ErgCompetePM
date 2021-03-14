using BLL.Communication;
using PM.BO.Commands;
using PM.BO.EventArguments;
using PM.BO.Interfaces;
using LibUsbDotNet.LibUsb;
using Microsoft.Extensions.Logging;
using PM.BO;
using PM.BO.Comparers;
using PM.BO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class PMService : IPMService
    {
        private const int VendorId = 0x17A4;
        private readonly ILogger<PMService> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IPMCommunicator _pmCommunicator;
        private readonly IPMPollingService _pmPollingService;
        private readonly ICommandListFactory _commandListFactory;

        public event EventHandler? DeviceFound
        {
            add { _pmCommunicator.DeviceFound += value; }
            remove { _pmCommunicator.DeviceFound -= value; }
        }
        public event EventHandler? DeviceLost
        {
            add { _pmCommunicator.DeviceLost += value; }
            remove { _pmCommunicator.DeviceLost -= value; }
        }

        private static readonly object _discoveryLock = new object();

        public event EventHandler? PollReturned
        {
            add { _pmPollingService.PollReturned += value; }
            remove { _pmPollingService.PollReturned -= value; }
        }

        public PMService(IPMCommunicator pmCommunicator, IPMPollingService pmPollingService, ICommandListFactory commandListFactory, ILogger<PMService> logger)
        {
            _logger = logger;
            _pmCommunicator = pmCommunicator;
            _pmPollingService = pmPollingService;
            _commandListFactory = commandListFactory;

            _pmCommunicator.DeviceLost += OnDeviceLost;
        }

        ~PMService()
        {
            
        }

        public void QuickTest()
        {
            IEnumerable<(int BusNumber, int Address)> locations = Discover();
            (int BusNumber, int Address) location = locations.First();

            ICommandList commands = _commandListFactory.Create();
            commands.AddRange(new ICommand[]
            {
                //new SetHorizontalDistanceCommand(5000, UnitType.Meters),
                //new SetSplitDurationDistanceCommand(150),
                //new SetPowerTargetCommand(150),
                //new SetProgramCommand(ProgrammedWorkout.Programmed),
                //new SetMachineStateInUseCommand()

                //new SetWorkoutTimeCommand(new uint[] {0x00, 0x07, 0x1E}),

                //new SetSplitDurationDistanceCommand(new uint[] {0x64, 0x00, 0x00, 0x00})
                new GetCadenceCommand(),
                new GetPowerCommand(),
                new GetUserInfoCommand(),
                new GetPaceCommand(),
                new GetStrokeStateCommand(),
                new GetWorkDistanceCommand(),
                new GetProductVersionCommand()
            });
            commands.Prepare();

            _pmCommunicator.Send(location, commands);
        }

        public IEnumerable<(int BusNumber, int Address)> Discover()
        {
            return _pmCommunicator.Discover();
        }

        public void StartAutoDiscovery(int secondsBetweenDiscovery = 10)
        {
            _pmCommunicator.StartAutoDiscovery(secondsBetweenDiscovery);
        }

        public void StopAutoDiscovery()
        {
            _pmCommunicator.StopAutoDiscovery();
        }

        public void Poll((int BusNumber, int Address) location)
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

        private void OnDeviceLost(object? sender, EventArgs args)
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
