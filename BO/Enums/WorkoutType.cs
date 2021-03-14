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
        JustRowNoSplits = 0,
        JustRowWithSplits = 1, 
        FixedDistanceNoSplits = 2,
        FixedDistanceWithSplits = 3,
        FixedTimeNoSplits = 4,
        FixedTimeWithSplits = 5,
        FixedTimeInterval = 6,
        FixedDistanceInterval = 7,
        VariableInterval
    }
}
