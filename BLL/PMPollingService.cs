using BO.Commands;
using BO.Enums;
using BO.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using BO;
using BLL.Communication;
using BLL.Extensions;

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

        private static readonly ConcurrentDictionary<ushort, Timer> _activePolls = new ConcurrentDictionary<ushort, Timer>();
        private static readonly ConcurrentDictionary<ushort, PMData> _data = new ConcurrentDictionary<ushort, PMData>();

        private readonly ILogger<PMPollingService> _logger;
        private readonly IPMCommunicator _pmCommunicator;

        public event EventHandler? PollReturned;

        public PMPollingService(IPMCommunicator pmCommunicator, ILogger<PMPollingService> logger)
        {
            _logger = logger;
            _pmCommunicator = pmCommunicator;
        }

        public void StartPolling(ushort port, IEnumerable<PollInterval> pollIntervals)
        {
            if (pollIntervals == null)
            {
                Exception e = new ArgumentNullException(nameof(pollIntervals), "Poll Intervals must not be null");
                _logger.LogError(e, "Invalid call to [{MethodName}] on port [{Port}]", nameof(StartPolling), port);
                throw e;
            }

            if (!pollIntervals.Any())
            {
                _logger.LogWarning("A call was made to start logging on port [{Port}], but no intervals were defined.", port);
            }

            if (_activePolls.ContainsKey(port))
            {
                Exception e = new InvalidOperationException("A poll was attempted to be started, but was already started. Poll must first be stopped.");
                _logger.LogError(e, "Poll for port [{Port}] was already started.", port);
                throw e;
            }

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            Timer timer = CreatePollTimer(port, pollIntervals);

            _activePolls.TryAdd(port, timer);
        }

        private Timer CreatePollTimer(ushort port, IEnumerable<PollInterval> pollIntervals)
        {
            const ushort MillisecondsBetweenTrigger = 10;
            PollState state = new PollState
            {
                ExecutionStartTime = DateTime.UtcNow,
                Port = port,
                PollIntervals = pollIntervals
            };

            return new Timer(HandlePollTimer, state, 0, MillisecondsBetweenTrigger);
        }

        public void StopPolling(ushort port)
        {
            if (!_activePolls.ContainsKey(port))
            {
                Exception e = new InvalidOperationException("A poll was attempted to be stopped, but was not running.");
                _logger.LogError(e, "Poll for port [{Port}] was not running.", port);
                throw e;
            }

            // Remove the task
            bool removeResult = _activePolls.Remove(port, out Timer? timer);

            if (!removeResult) 
            {
                _logger.LogWarning("A poll was attempted to be stopped for port [{Port}], but was not found.", port);
                return; 
            }

            if (timer == null)
            {
                _logger.LogWarning("A poll was found for port [{Port}], but its timer was null.", port);
                return;
            }

            // Dispose of the timer
            timer.Dispose();
        }

        public virtual void OnPollReturned(EventArgs args)
        {
            PollReturned?.Invoke(this, args);
        }

        private void HandlePollTimer(object? stateInfo)
        {
            if (stateInfo == null)
            {
                Exception e = new ArgumentNullException(nameof(stateInfo));
                _logger.LogError(e, "State must be provided to [{MethodName}]", nameof(HandlePollTimer));
                throw e;
            }

            if (stateInfo is not PollState pollState)
            {
                Exception e = new ArgumentException("Object is the incorrect type.", nameof(stateInfo));
                _logger.LogError(e, "Object must be of type [{ObjectType}]", typeof(PollState));
                throw e;
            }

            // Collect the intervals to execute for this iteration
            IEnumerable<PollInterval> pollIntervals = CollectPollIntervals(pollState.Iterations++);

            // Execute poll
            CommandList? result = ExecutePoll(pollState.Port, pollIntervals);

            // Update data
            UpdateData(pollState.Port, result);

            // Check if 1 second has been reached
            if (pollState.Iterations > 100)
            {
                // Fire event with the poll data
                EventArgs args = new PollEventArgs
                {
                    Data = _data[pollState.Port],
                    Port = pollState.Port
                };

                OnPollReturned(args);

                // Reset the count
                pollState.Iterations = 0;
            }
        }

        private static void UpdateData(ushort port, CommandList? results)
        {
            if (!_data.ContainsKey(port))
            {
                _data.TryAdd(port, new PMData());
            }

            foreach(ICommand command in results ?? Enumerable.Empty<ICommand>())
            {
                command.UpdatePMData(_data[port]);
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

        private CommandList? ExecutePoll(ushort port, IEnumerable<PollInterval> pollIntervals)
        {
            CommandList commands = new CommandList();

            foreach (PollInterval pollInterval in pollIntervals)
            {
                commands.AddRange(_pollIntervals[pollInterval]);
            }

            commands.Prepare();

            try
            {
                _pmCommunicator.Send(port, commands);
                return commands;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred while sending poll commands for port [{Port}]", port);
            }

            return null;
        }
    }
}
