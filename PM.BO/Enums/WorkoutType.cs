namespace PM.BO.Enums
{
    /// <summary>
    /// Current stroke state
    /// </summary>
    /// <remarks>
    /// Catch would be the transition from recovery to driving. End-of-stroke would be the transition from driving to dwelling after drive
    /// </remarks>
    public enum WorkoutType
    {
        /// <summary>
        /// JustRow, no splits
        /// </summary>
        JustRowNoSplits = 0,

        /// <summary>
        /// JustRow, splits
        /// </summary>
        JustRowWithSplits = 1,

        /// <summary>
        /// Fixed distance, no splits
        /// </summary>
        FixedDistanceNoSplits = 2,

        /// <summary>
        /// Fixed distance, splits
        /// </summary>
        FixedDistanceWithSplits = 3,

        /// <summary>
        /// Fixed time, no splits 
        /// </summary>
        FixedTimeNoSplits = 4,

        /// <summary>
        /// Fixed time, splits
        /// </summary>
        FixedTimeWithSplits = 5,

        /// <summary>
        /// Fixed time interval
        /// </summary>
        FixedTimeInterval = 6,

        /// <summary>
        /// Fixed distance interval
        /// </summary>
        FixedDistanceInterval = 7,

        /// <summary>
        /// Variable interval
        /// </summary>
        VariableInterval = 8,

        /// <summary>
        /// Variable interval, undefined rest
        /// </summary>
        VariableIntervalUndefinedRest = 9,

        /// <summary>
        /// Fixed calorie, splits
        /// </summary>
        FixedCalorieWithSplits = 10,

        /// <summary>
        /// Fixed watt-minute, splits
        /// </summary>
        FixedWattMinuteWithSplits = 11,

        /// <summary>
        /// Fixed calorie interval
        /// </summary>
        FixedCalorieInterval = 12,

        /// <summary>
        /// Number of workout types
        /// </summary>
        NumberOfWorkoutTypes = 13
    }
}
