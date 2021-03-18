using Microsoft.Extensions.Logging;
using PM.BO;
using PM.BO.Commands;
using PM.BO.Enums;
using PM.BO.EventArguments;
using PM.BO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// The PM Service initiating & handling access and data to the PM
    /// </summary>
    public class PMService : IPMService
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<PMService> _logger;

        /// <summary>
        /// The communicator service
        /// </summary>
        private readonly IPMCommunicator _pmCommunicator;

        /// <summary>
        /// The command list factory
        /// </summary>
        private readonly ICommandListFactory _commandListFactory;

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
        public PMService(IPMCommunicator pmCommunicator, ICommandListFactory commandListFactory, IPMPollingService pmPollingService, ILogger<PMService> logger)
        {
            _logger = logger;
            _pmCommunicator = pmCommunicator;
            _commandListFactory = commandListFactory;
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
        public IEnumerable<string> Discover()
        {
            return _pmCommunicator.Discover();
        }

        /// <inheritdoc />
        public void StartAutoDiscovery(int millisecondsBetweenDiscovery = 100)
        {
            _pmCommunicator.StartAutoDiscovery(millisecondsBetweenDiscovery);
        }

        /// <inheritdoc />
        public void StopAutoDiscovery()
        {
            _pmCommunicator.StopAutoDiscovery();
        }

        /// <inheritdoc />
        public void StartPolling(string serialNumber)
        {
            _logger.LogInformation("Establishing polling intervals.");

            IEnumerable<PollInterval> pollIntervals = new [] 
            { 
                PollInterval.Hz_100, 
                PollInterval.Hz_10, 
                PollInterval.Hz_2, 
                PollInterval.Hz_1 
            };

            _logger.LogInformation("Starting polling for serial number [{SerialNumber}].", serialNumber);
            _pmPollingService.StartPolling(serialNumber, pollIntervals);
        }

        /// <inheritdoc />
        public void StopPolling(string? serialNumber = null)
        {
            if (serialNumber == null)
            {
                _logger.LogInformation("Stopping polling for serial number [{SerialNumber}].", serialNumber);
            }
            else
            {
                _logger.LogInformation("Stopping polling on all locations.");
            }

            _pmPollingService.StopPolling(serialNumber);
        }

        #region Workouts
        /// <inheritdoc />
        public void SetJustRowWorkout(string serialNumber, bool splits)
        {
            IEnumerable<ICommand> commands = BuildJustRowWorkout(splits);

            ICommandList commandList = _commandListFactory.Create(commands);
            commandList.Prepare();
            _pmCommunicator.Send(serialNumber, commandList);
        }

        /// <inheritdoc />
        public void SetFixedWorkout(string serialNumber, Interval interval)
        {
            if (interval == null)
            {
                _logger.LogError("Interval must be defined when setting a fixed workout. No workout has been set.");
                return;
            }

            switch(interval.WorkoutType)
            {
                case WorkoutType.FixedCalorieInterval:
                case WorkoutType.FixedCalorieWithSplits:
                case WorkoutType.FixedDistanceInterval:
                case WorkoutType.FixedDistanceNoSplits:
                case WorkoutType.FixedDistanceWithSplits:
                case WorkoutType.FixedTimeInterval:
                case WorkoutType.FixedTimeNoSplits:
                case WorkoutType.FixedTimeWithSplits:
                case WorkoutType.FixedWattMinuteWithSplits:
                    break;
                default:
                    _logger.LogError("Only fixed interval workout types can be provided when setting a fixed workout. Now workout has been set.");
                    return;
            }

            IEnumerable<ICommand>? commands = BuildFixedWorkout(interval);

            if (commands == null)
            {
                _logger.LogError("An error occurred while building commands for fixed workout. No workout has been set.");
                return;
            }

            ICommandList commandList = _commandListFactory.Create(commands);
            commandList.Prepare();
            _pmCommunicator.Send(serialNumber, commandList);
        }

        /// <inheritdoc />
        public void SetVariableWorkout(string serialNumber, IEnumerable<Interval> intervals)
        {
            if (intervals == null)
            {
                _logger.LogError("Intervals must be defined. No workout has been set.");
                return;
            }

            IEnumerable<ICommand>? commands = BuildVariableWorkout(intervals);

            if (commands == null)
            {
                _logger.LogError("An error occurred while building commands for variable workout. No workout has been set.");
                return;
            }

            ICommandList commandList = _commandListFactory.Create(commands);
            commandList.Prepare();
            _pmCommunicator.Send(serialNumber, commandList);
        }

        /// <inheritdoc />
        public void TerminateWorkout(string serialNumber)
        {
            ICommandList commandList = _commandListFactory.Create();
            commandList.Add(new SetScreenStateCommand(ScreenType.Workout, ScreenValueWorkout.TerminateWorkout));
            commandList.Prepare();
            _pmCommunicator.Send(serialNumber, commandList);
        }
        #endregion

        /// <summary>
        /// Builds the commands required for a Just Row workout
        /// </summary>
        /// <param name="splits">True if with splits, false if without</param>
        /// <returns>Enumerable of the required commands</returns>
        private static IEnumerable<ICommand> BuildJustRowWorkout(bool splits)
        {
            WorkoutType workoutType = splits ? WorkoutType.JustRowWithSplits : WorkoutType.JustRowNoSplits;

            return new List<ICommand>
            {
                new SetWorkoutTypeCommand(workoutType),
                new SetScreenStateCommand(ScreenType.Workout, ScreenValueWorkout.PrepareToRowWorkout)
            };
        }

        /// <summary>
        /// Builds the commands required for a fixed workout
        /// </summary>
        /// <param name="interval">The interval</param>
        /// <returns>An enumerable of the commands</returns>
        private IEnumerable<ICommand>? BuildFixedWorkout(Interval interval)
        {
            if (interval == null)
            {
                _logger.LogError("Interval must be defined when setting a fixed workout. No workout has been set.");
                return null;
            }

            if (interval.WorkoutType == null || interval.Duration == null)
            {
                _logger.LogError("A minimum of workout type and duration must be defined when setting a fixed workout. No workout has been set.");
                return null;
            }

            WorkoutDetails? workoutDetails = GetWorkoutDetails(interval);

            if (workoutDetails == null)
            {
                _logger.LogError("An error occurred while extracting the workout details. No workout has been set.");
                return null;
            }

            IEnumerable<ICommand>? builtCommands = BuildInterval(interval, workoutDetails.Value);
            
            if (builtCommands == null)
            {
                _logger.LogError("An error occurred while building the interval. No workout has been set.");
                return null;
            }
            
            List<ICommand> commands = new(builtCommands);

            commands.AddRange(new List<ICommand>
            {
                new SetConfigureWorkoutCommand(WorkoutProgrammingMode.Enable),
                new SetScreenStateCommand(ScreenType.Workout, ScreenValueWorkout.PrepareToRowWorkout)
            });

            return commands;
        }

        /// <summary>
        /// Builds only the commands in a variable interval workout
        /// </summary>
        /// <param name="intervals">The intervals</param>
        /// <returns>Enumerable of the commands</returns>
        private IEnumerable<ICommand>? BuildVariableWorkout(IEnumerable<Interval> intervals)
        {
            IList<Interval> intervalList = new List<Interval>(intervals);

            if (intervalList.Any(i => i.IntervalType == null))
            {
                _logger.LogError("All intervals must have a defined interval type. No workout has been set.");
                return null;
            }

            if (!intervalList.Any())
            {
                _logger.LogError("At least one interval must be defined. No workout has been set.");
                return null;
            }

            bool atLeastOneIntervalWithUndefinedRest = false;

            List<ICommand> commands = new();
            for (int intervalNumber = 0; intervalNumber < intervalList.Count; intervalNumber++)
            {
                Interval currentInterval = intervalList[intervalNumber];
                if (currentInterval.IntervalType == null)
                {
                    _logger.LogCritical("The interval type was unexpectedly null. No workout has been set.");
                    return null;
                }

                commands.Add(new SetIntervalWorkoutCountCommand(intervalNumber));

                if (intervalNumber == 0)
                {
                    commands.Add(new SetWorkoutTypeCommand(WorkoutType.VariableInterval));
                }

                commands.Add(new SetIntervalTypeCommand(currentInterval.IntervalType.Value));

                WorkoutDetails? workoutDetails = GetWorkoutDetails(currentInterval);
                if (workoutDetails == null)
                {
                    _logger.LogError("An error occurred while extracting workout details. No workout has been set.");
                    return null;
                }

                atLeastOneIntervalWithUndefinedRest = atLeastOneIntervalWithUndefinedRest || !workoutDetails.Value.RequireRest;

                IEnumerable<ICommand>? intervalCommands = BuildInterval(currentInterval, workoutDetails.Value);

                if (intervalCommands == null)
                {
                    _logger.LogError("An error occurred while building the interval commands. No workout has been set.");
                    return null;
                }

                commands.AddRange(intervalCommands);

                commands.Add(new SetConfigureWorkoutCommand(WorkoutProgrammingMode.Enable));
            }

            if (atLeastOneIntervalWithUndefinedRest)
            {
                // Setting the SplitDurationDistance to 0 is necessary when at least one undefined rest interval is configured
                commands.AddRange(new ICommand[]
                {
                    new SetWorkoutTypeCommand(WorkoutType.VariableIntervalUndefinedRest),
                    new SetSplitDurationCommand(WorkoutDuration.Distance, 0)
                });
            }

            commands.Add(new SetScreenStateCommand(ScreenType.Workout, ScreenValueWorkout.PrepareToRowWorkout));

            return commands;
        }

        /// <summary>
        /// Builds only the commands in an interval
        /// </summary>
        /// <param name="interval">The interval</param>
        /// <param name="workoutDetails">The workout details</param>
        /// <returns>Enumerable of the commands</returns>
        private IEnumerable<ICommand>? BuildInterval(Interval interval, WorkoutDetails workoutDetails)
        {
            if (interval.WorkoutType == null && interval.IntervalType == null)
            {
                _logger.LogCritical("Workout type and Interval type was unexpectedly null. One must be specified based on Variable vs Fixed. No workout has been set.");
                return null;
            }

            if (interval.Duration == null)
            {
                _logger.LogCritical("Duration was unexpected null. No workout has been set.");
                return null;
            }

            int durationValue;
            int? splitValue = null;
            if (workoutDetails.WorkoutDuration == WorkoutDuration.Time)
            {
                // Time is in 0.01s resolution, so requires converstion
                durationValue = interval.Duration.Value * 100;
                if (workoutDetails.RequireSplits && interval.Splits != null)
                {
                    splitValue = interval.Splits.Value * 100;
                }
            }
            else
            {
                // Other durations are valid as-is
                durationValue = interval.Duration.Value;
                if (workoutDetails.RequireSplits && interval.Splits != null)
                {
                    splitValue = interval.Splits.Value;
                }
            }

            if (splitValue == null && workoutDetails.RequireSplits)
            {
                _logger.LogCritical("Split Value was unexpectedly null. No workout has been set.");
                return null;
            }

            List<ICommand> commands = new();
            if (interval.WorkoutType != null)
            {
                commands.Add(new SetWorkoutTypeCommand(interval.WorkoutType.Value));
            }

            commands.Add(new SetWorkoutDurationCommand(workoutDetails.WorkoutDuration, durationValue));

            if (workoutDetails.RequireSplits && splitValue != null)
            {
                commands.Add(new SetSplitDurationCommand(workoutDetails.WorkoutDuration, splitValue.Value));
            }
            else if ((workoutDetails.RequireRest || interval.IntervalType != null) && interval.SecondsOfRest != null)
            {
                commands.Add(new SetRestDurationCommand(interval.SecondsOfRest.Value));
            }
            
            if (interval.TargetPace != null)
            {
                commands.Add(new SetTargetPaceCommand(interval.TargetPace.Value));
            }

            return commands;
        }

        /// <summary>
        /// Gets workout details pertaining to certain requirements
        /// </summary>
        /// <param name="interval">The interval</param>
        /// <returns>The workout details</returns>
        private WorkoutDetails? GetWorkoutDetails(Interval interval)
        {
            bool requireSplits = false;
            bool requireRest = false;
            WorkoutDuration? workoutDuration = null;

            if (interval.IntervalType != null && interval.WorkoutType == null)
            {
                switch(interval.IntervalType)
                {
                    // Variable intervals with rest
                    case IntervalType.Calorie:
                        workoutDuration = WorkoutDuration.Calories;
                        requireRest = true;
                        break;
                    case IntervalType.Distance:
                        workoutDuration = WorkoutDuration.Distance;
                        requireRest = true;
                        break;
                    case IntervalType.Time:
                        workoutDuration = WorkoutDuration.Time;
                        requireRest = true;
                        break;
                    case IntervalType.WattMinute:
                        workoutDuration = WorkoutDuration.WattMinutes;
                        requireRest = true;
                        break;

                    // Variable intervals without rest. Mandated 0:00
                    case IntervalType.CalorieUndefinedRest:
                        workoutDuration = WorkoutDuration.Calories;
                        interval.SecondsOfRest = 0;
                        break;
                    case IntervalType.DistanceUndefinedRest:
                        workoutDuration = WorkoutDuration.Distance;
                        interval.SecondsOfRest = 0;
                        break;
                    case IntervalType.TimeUndefinedRest:
                        workoutDuration = WorkoutDuration.Time;
                        interval.SecondsOfRest = 0;
                        break;
                    case IntervalType.WattMinuteUndefinedRest:
                        workoutDuration = WorkoutDuration.WattMinutes;
                        interval.SecondsOfRest = 0;
                        break;
                }
            }

            if (interval.WorkoutType != null && interval.IntervalType == null) { 
                switch (interval.WorkoutType)
                {
                    // Fixed, No Splits
                    case WorkoutType.FixedDistanceNoSplits:
                        workoutDuration = WorkoutDuration.Distance;
                        break;
                    case WorkoutType.FixedTimeNoSplits:
                        workoutDuration = WorkoutDuration.Time;
                        break;

                    // Fixed, Splits
                    case WorkoutType.FixedCalorieWithSplits:
                        workoutDuration = WorkoutDuration.Calories;
                        requireSplits = true;
                        break;
                    case WorkoutType.FixedDistanceWithSplits:
                        workoutDuration = WorkoutDuration.Distance;
                        requireSplits = true;
                        break;
                    case WorkoutType.FixedTimeWithSplits:
                        workoutDuration = WorkoutDuration.Time;
                        requireSplits = true;
                        break;
                    case WorkoutType.FixedWattMinuteWithSplits:
                        workoutDuration = WorkoutDuration.WattMinutes;
                        requireSplits = true;
                        break;

                    // Fixed, Intervals
                    case WorkoutType.FixedCalorieInterval:
                        workoutDuration = WorkoutDuration.Calories;
                        requireRest = true;
                        break;
                    case WorkoutType.FixedDistanceInterval:
                        workoutDuration = WorkoutDuration.Distance;
                        requireRest = true;
                        break;
                    case WorkoutType.FixedTimeInterval:
                        workoutDuration = WorkoutDuration.Time;
                        requireRest = true;
                        break;

                    // Error case
                    default:
                        _logger.LogError("Invalid workout type encountered when setting fixed interval workout. No workout has been set.");
                        break;
                }
            }

            if (workoutDuration == null)
            {
                _logger.LogCritical("Workout duration was not set in one of the cases. No workout has been set.");
                return null;
            }

            if (requireSplits && requireRest)
            {
                _logger.LogCritical("A configuration error occurred in one of the cases as splits and rest should never both be required. No workout has been set.");
                return null;
            }

            if (requireSplits && interval.Splits == null)
            {
                _logger.LogError("A workout type requiring splits was provided, but no split was defined. No workout has been set.");
                return null;
            }

            if (requireRest && interval.SecondsOfRest == null)
            {
                _logger.LogError("A workout type requiring rest was provided, but no rest was defined. No workout has been set.");
                return null;
            }

            return new WorkoutDetails(workoutDuration.Value, requireRest, requireSplits);
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

            if (deviceArgs.SerialNumber == null)
            {
                _logger.LogCritical("Serial number was null when DeviceLost was acted on.");
                return;
            }

            try
            {
                // Close poll if active
                if (_pmPollingService.IsActive(deviceArgs.SerialNumber))
                {
                    _pmPollingService.StopPolling(deviceArgs.SerialNumber);
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Attempted to stop polling for serial number [{SerialNumber}], but failed.", deviceArgs.SerialNumber);
            }
        }
    }
}