using BLL.Communication;
using BLL.Extensions;
using PM.BO;
using PM.BO.Commands;
using PM.BO.EventArguments;
using PM.BO.Enums;
using PM.BO.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PM.BLL.Factories;

namespace BLL
{
    public class PMPollingService : IPMPollingService
    {
        private static readonly IDictionary<PollInterval, IList<ICommand>> _pollIntervals = new Dictionary<PollInterval, IList<ICommand>>
            {
            #region 100Hz
            { PollInterval.Hz_100, new List<ICommand>
                {
                    new GetStrokeStateCommand()
                } },
            #endregion
            #region 10Hz
            { PollInterval.Hz_10, new List<ICommand>
                {
                    new GetWorkoutDurationCommand(),
                    new GetHorizontalDistanceCommand(),
                    new GetWorkTimeCommand(),
                    new GetWorkDistanceCommand(),
                    new GetRestTimeCommand()
                } },
            #endregion
            #region 2Hz
            { PollInterval.Hz_2, new List<ICommand>
                {
                    new GetPaceCommand(),
                    new GetPowerCommand(),
                    new GetAccumulatedCaloriesCommand(),
                    new GetCadenceCommand(),
                    new GetDragFactorCommand(),
                    new GetWorkoutStateCommand(),
                    new GetStrokeStatisticsCommand()
                } },
            #endregion
            #region 1Hz
            { PollInterval.Hz_1, new List<ICommand>
                {
                    new GetCurrentHeartRateCommand()
                } },
            #endregion
        };

        private static bool _currentlyPolling = false;
        private static readonly ConcurrentDictionary<(int BusNumber, int Address), Timer> _activePolls = new ConcurrentDictionary<(int BusNumber, int Address), Timer>();
        private static readonly ConcurrentDictionary<(int BusNumber, int Address), PMData> _data = new ConcurrentDictionary<(int BusNumber, int Address), PMData>();

        private readonly ILogger<PMPollingService> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IPMCommunicator _pmCommunicator;

        public event EventHandler? PollReturned;
        private ICommandListFactory _commandListFactory;

        public PMPollingService(IPMCommunicator pmCommunicator, ICommandListFactory commandListFactory, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PMPollingService>();
            _loggerFactory = loggerFactory;
            _pmCommunicator = pmCommunicator;
            _commandListFactory = commandListFactory;
        }

        public void StartPolling((int BusNumber, int Address) location, IEnumerable<PollInterval> pollIntervals)
        {
            if (pollIntervals == null)
            {
                Exception e = new ArgumentNullException(nameof(pollIntervals), "Poll Intervals must not be null");
                _logger.LogCritical(e, "Invalid call to [{MethodName}] for location [{Location}]", nameof(StartPolling), location);
                throw e;
            }

            if (!pollIntervals.Any())
            {
                _logger.LogWarning("A call was made to start logging for location [{Location}], but no intervals were defined.", location);
                return;
            }

            if (_activePolls.ContainsKey(location))
            {
                _logger.LogWarning("A poll was attempted to be started for location [{Location}], but was already started. Poll must first be stopped. Ignoring duplicate poll.", location);
                return;
            }

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            Timer timer = CreatePollTimer(location, pollIntervals);

            _activePolls.TryAdd(location, timer);
        }

        public bool IsActive((int BusNumber, int Address) location)
        {
            return _activePolls.ContainsKey(location);
        }

        private Timer CreatePollTimer((int BusNumber, int Address) location, IEnumerable<PollInterval> pollIntervals)
        {
            const ushort MillisecondsBetweenTrigger = 10;
            PollState state = new PollState(location)
            {
                ExecutionStartTime = DateTime.UtcNow,
                PollIntervals = pollIntervals
            };

            return new Timer(HandlePollTimer, state, 0, MillisecondsBetweenTrigger);
        }

        public void StopPolling((int BusNumber, int Address) location)
        {
            if (!_activePolls.ContainsKey(location))
            {
                _logger.LogWarning("A poll was attempted to be stopped, but was not running for location [{Location}].", location);
                return;
            }

            // Remove the task
            bool removeResult = _activePolls.Remove(location, out Timer? timer);

            if (!removeResult) 
            {
                _logger.LogWarning("A poll was attempted to be stopped for location [{Location}], but was not found.", location);
                return; 
            }

            if (timer == null)
            {
                _logger.LogWarning("A poll was found for location [{Location}], but its timer was null.", location);
                return;
            }

            // Dispose of the timer
            timer.Dispose();
        }

        private void HandlePollTimer(object? stateInfo)
        {
            if (_currentlyPolling)
            {
                return;
            }

            if (stateInfo == null)
            {
                _logger.LogWarning("State must be provided to [{MethodName}]. No action is being taken for this interval.", nameof(HandlePollTimer));
                return;
            }

            if (stateInfo is not PollState pollState)
            {
                _logger.LogWarning("Object must be of type [{ObjectType}]. No action is being taken for this interval.", typeof(PollState));
                return;
            }

            _currentlyPolling = true;

            // Collect the intervals to execute for this iteration
            IEnumerable<PollInterval> pollIntervals = CollectPollIntervals(pollState.Iterations++);

            if (pollIntervals.Any()) 
            { 
                // Execute poll
                ICommandList? result = ExecutePoll(pollState.Location, pollIntervals);

                // Update data
                UpdateData(pollState.Location, result);
            
                // Check if 1 second has been reached
                if (pollState.Iterations > 100)
                {
                    // Fire event with the poll data
                    EventArgs args = new PollEventArgs
                    {
                        Data = _data[pollState.Location],
                        Location = pollState.Location
                    };

                    PollReturned?.Invoke(this, args);

                    // Reset the count
                    pollState.Iterations = 0;
                }
            }

            _currentlyPolling = false;
        }

        private static void UpdateData((int BusNumber, int Address) location, ICommandList? results)
        {
            if (!_data.ContainsKey(location))
            {
                _data.TryAdd(location, new PMData());
            }

            foreach(ICommand command in results ?? Enumerable.Empty<ICommand>())
            {
                command.UpdatePMData(_data[location]);
            }
        }

        private static IEnumerable<PollInterval> CollectPollIntervals(ushort iteration)
        {
            IList<PollInterval> pollIntervals = new List<PollInterval>();

            if (iteration % 1 == 0)
            {
                pollIntervals.Add(PollInterval.Hz_100);
            }

            if (iteration % 10 == 0)
            {
                pollIntervals.Add(PollInterval.Hz_10);
            }

            if (iteration % 50 == 0)
            {
                pollIntervals.Add(PollInterval.Hz_2);
            }

            if (iteration % 100 == 0)
            {
                pollIntervals.Add(PollInterval.Hz_1);
            }

            return pollIntervals;
        }

        private ICommandList? ExecutePoll((int BusNumber, int Address) location, IEnumerable<PollInterval> pollIntervals)
        {
            ICommandList commands = _commandListFactory.Create();

            foreach (PollInterval pollInterval in pollIntervals)
            {
                commands.AddRange(_pollIntervals[pollInterval]);
            }

            commands.Prepare();

            try
            {
                _pmCommunicator.Send(location, commands);   
                return commands;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred while sending poll commands to location [{Location}]", location);
            }

            return null;
        }
    }
}
