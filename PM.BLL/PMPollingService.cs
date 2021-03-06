using BLL.Extensions;
using Microsoft.Extensions.Logging;
using PM.BO;
using PM.BO.Commands;
using PM.BO.Enums;
using PM.BO.EventArguments;
using PM.BO.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading;

namespace BLL
{
    /// <summary>
    /// Handles interval polling on the PM
    /// </summary>
    public class PMPollingService : IPMPollingService
    {
        /// <summary>
        /// Lock for accessing workout state in MemoryCache
        /// </summary>
        private static readonly object _workoutStateLock;

        /// <summary>
        /// List of standard poll commands at defined intervals
        /// </summary>
        private static readonly IDictionary<PollInterval, IList<ICommand>> _pollIntervals;

        /// <summary>
        /// Seconds between checking for current workout state
        /// </summary>
        private const int WorkoutStateCheckInterval = 30;

        /// <summary>
        /// Serial numbers that have active polls
        /// </summary>
        private static readonly ConcurrentDictionary<string, Timer> _activePolls;
        
        /// <summary>
        /// PM Data for each serial number
        /// </summary>
        private static readonly ConcurrentDictionary<string, PMData> _data;
        
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<PMPollingService> _logger;

        /// <summary>
        /// The Communicator service
        /// </summary>
        private readonly IPMCommunicator _pmCommunicator;

        /// <summary>
        /// The command list factory
        /// </summary>
        private readonly ICommandListFactory _commandListFactory;

        /// <inheritdoc />
        public event EventHandler? PollReturned;
        
        static PMPollingService()
        {
            _workoutStateLock = new();
            _activePolls = new();
            _data = new();
            _pollIntervals = new Dictionary<PollInterval, IList<ICommand>>
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
        }

        /// <summary>
        /// DI Constructor
        /// </summary>
        /// <param name="pmCommunicator">The PM Communicator</param>
        /// <param name="commandListFactory">The command list factory</param>
        /// <param name="logger">The logger</param>
        public PMPollingService(IPMCommunicator pmCommunicator, ICommandListFactory commandListFactory, ILogger<PMPollingService> logger)
        {
            _logger = logger;
            _pmCommunicator = pmCommunicator;
            _commandListFactory = commandListFactory;
        }

        /// <inheritdoc />
        public void StartPolling(string serialNumber, IEnumerable<PollInterval> pollIntervals)
        {
            if (pollIntervals == null)
            {
                Exception e = new ArgumentNullException(nameof(pollIntervals), "Poll Intervals must not be null");
                _logger.LogCritical(e, "Invalid call to [{MethodName}] for serial number [{SerialNumber}]", nameof(StartPolling), serialNumber);
                throw e;
            }

            if (!pollIntervals.Any())
            {
                _logger.LogWarning("A call was made to start logging for serial number [{SerialNumber}], but no intervals were defined.", serialNumber);
                return;
            }

            if (_activePolls.ContainsKey(serialNumber))
            {
                _logger.LogWarning("A poll was attempted to be started for serial number [{SerialNumber}], but was already started. Poll must first be stopped. Ignoring duplicate poll.", serialNumber);
                return;
            }

            CancellationTokenSource cancellationTokenSource = new();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            Timer timer = CreatePollTimer(serialNumber, pollIntervals);

            _activePolls.TryAdd(serialNumber, timer);
        }

        /// <inheritdoc />
        public void StopPolling(string? serialNumber = null)
        {
            if (serialNumber == null)
            {
                // stop all polling
                foreach (string activeSerialNumber in _activePolls.Keys)
                {
                    // stop polling on single active location
                    StopPolling(activeSerialNumber);
                }

                return;
            }

            if (!_activePolls.ContainsKey(serialNumber))
            {
                _logger.LogWarning("A poll was attempted to be stopped, but was not running for serial number [{SerialNumber}].", serialNumber);
                return;
            }

            // Remove the task
            bool removeResult = _activePolls.Remove(serialNumber, out Timer? timer);

            if (!removeResult)
            {
                _logger.LogWarning("A poll was attempted to be stopped for serial number [{SerialNumber}], but was not found.", serialNumber);
                return;
            }

            if (timer == null)
            {
                _logger.LogWarning("A poll was found for serial number [{SerialNumber}], but its timer was null.", serialNumber);
                return;
            }

            // Dispose of the timer
            timer.Dispose();
        }

        /// <inheritdoc />
        public bool IsActive(string serialNumber)
        {
            return _activePolls.ContainsKey(serialNumber);
        }

        /// <summary>
        /// Creates a poll timer for the specified location
        /// </summary>
        /// <param name="serialNumber">The serialNumber</param>
        /// <param name="pollIntervals">The intervals to poll for</param>
        /// <returns>The created timer</returns>
        private Timer CreatePollTimer(string serialNumber, IEnumerable<PollInterval> pollIntervals)
        {
            const ushort MillisecondsBetweenTrigger = 10;
            PollState state = new(serialNumber)
            {
                ExecutionStartTime = DateTime.UtcNow,
                PollIntervals = pollIntervals
            };

            return new Timer(HandlePollTimer, state, 0, MillisecondsBetweenTrigger);
        }

        /// <summary>
        /// Updates the workout state stored in cache
        /// </summary>
        /// <param name="args">The update arguments</param>
        private void UpdateWorkoutState(CacheEntryUpdateArguments args)
        {
            string serialNumber = args.Key;

            if (!_activePolls.ContainsKey(serialNumber))
            {
                // This device is no longer connected, so do not refresh the state
                return;
            }

            if (args?.UpdatedCacheItem == null)
            {
                return;
            }

            args.UpdatedCacheItem.Value = GetWorkoutState(serialNumber);
        }

        /// <summary>
        /// Gets the current workout state for a specified location
        /// </summary>
        /// <param name="serialNumber">The serial number</param>
        /// <returns>The current workout state</returns>
        private WorkoutState? GetWorkoutState(string serialNumber)
        {
            ICommandList commands = _commandListFactory.Create();
            commands.Add(new GetWorkoutStateCommand());
            commands.Prepare();

            try
            {
                _pmCommunicator.Send(serialNumber, commands);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Error occurred while checking workout state prior to polling");
                return WorkoutState.WaitingToBegin;
            }

            return commands.FirstOrDefault()?.Value;
        }

        /// <summary>
        /// Listener for the poll timer interval
        /// </summary>
        /// <param name="stateInfo">The state of the timer, containing location</param>
        private void HandlePollTimer(object? stateInfo)
        {
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

            ICommandList commands = _commandListFactory.Create();

            string cacheKey = pollState.SerialNumber;
            ObjectCache workoutStateCache = MemoryCache.Default;
            WorkoutState? workoutState;

            lock (_workoutStateLock) 
            { 
                workoutState = workoutStateCache[cacheKey] as WorkoutState?;
            
                if (workoutState == null)
                {
                    CacheItemPolicy policy = new()
                    {
                        AbsoluteExpiration = DateTime.UtcNow.AddSeconds(WorkoutStateCheckInterval),
                        UpdateCallback = UpdateWorkoutState
                    };

                    int retryCount = 0;
                    bool retryWorkoutState = false;
                    do
                    {
                        workoutState = GetWorkoutState(pollState.SerialNumber);

                        if (workoutState == null)
                        {
                            retryWorkoutState = retryCount++ < 2;
                        }
                        else
                        {
                            retryWorkoutState = false;
                        }
                    } while (retryWorkoutState);

                    if (workoutState == null)
                    {
                        throw new InvalidOperationException("WorkoutState was null after retries.");
                    }

                    workoutStateCache.Set(cacheKey, workoutState, policy);
                }
                else
                {
                    workoutState = workoutStateCache[cacheKey] as WorkoutState?;
                }
            }

            if (workoutState == null)
            {
                _logger.LogWarning("Workout state could not be found. Polling not performed.");
                return;
            }

            int workoutStateValue = (int) workoutState;
            if (workoutStateValue < 1 || workoutStateValue > 9)
            {
                // TODO: Uncomment this
                // return;
            }

            // Collect the intervals to execute for this iteration
            IEnumerable<PollInterval> pollIntervals = CollectPollIntervals(pollState.Iterations++);

            if (pollIntervals.Any()) 
            { 
                // Execute poll
                ICommandList? result = ExecutePoll(pollState.SerialNumber, pollIntervals);

                // Update data
                UpdateData(pollState.SerialNumber, result);
            
                // Check if 1 second has been reached
                if (pollState.Iterations > 100)
                {
                    // Fire event with the poll data
                    EventArgs args = new PollEventArgs
                    {
                        Data = _data[pollState.SerialNumber],
                        SerialNumber = pollState.SerialNumber
                    };

                    PollReturned?.Invoke(this, args);

                    // Reset the count
                    pollState.Iterations = 0;
                }
            }
        }

        /// <summary>
        /// Updates data in the PM with the results from the poll
        /// </summary>
        /// <param name="serialNumber">The serial number</param>
        /// <param name="results">The poll results</param>
        private static void UpdateData(string serialNumber, ICommandList? results)
        {
            if (!_data.ContainsKey(serialNumber))
            {
                _data.TryAdd(serialNumber, new PMData());
            }

            foreach(ICommand command in results ?? Enumerable.Empty<ICommand>())
            {
                command.UpdatePMData(_data[serialNumber]);
            }
        }

        /// <summary>
        /// Gathers appropriate poll intervals based on intended frequency
        /// </summary>
        /// <param name="iteration">The current timer iteration</param>
        /// <returns>The poll intervals to poll for</returns>
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

        /// <summary>
        /// Actually polls the device
        /// </summary>
        /// <param name="serialNumber">The serial number</param>
        /// <param name="pollIntervals">The intervals to poll for</param>
        /// <returns>The populated commands</returns>
        private ICommandList? ExecutePoll(string serialNumber, IEnumerable<PollInterval> pollIntervals)
        {
            ICommandList commands = _commandListFactory.Create();

            foreach (PollInterval pollInterval in pollIntervals)
            {
                try 
                { 
                    commands.AddRange(_pollIntervals[pollInterval]);
                }
                catch (Exception e)
                {
                    _logger.LogCritical("Could not add poll intervals");
                }
            }

            commands.Prepare();

            try
            {
                _pmCommunicator.Send(serialNumber, commands);   
                return commands;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred while sending poll commands to serial number [{SerialNumber}]", serialNumber);
            }

            return null;
        }
    }
}
