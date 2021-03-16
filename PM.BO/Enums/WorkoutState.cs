namespace PM.BO.Enums
{
    public enum WorkoutState
    {
        WaitingToBegin = 0,
        WorkoutRow = 1,
        CountdownPause = 2,
        IntervalRest = 3,
        WorkTimeInterval = 4,
        WorkDistanceInterval = 5,
        RestIntervalEndToWorkTimeIntervalBegin = 6,
        RestIntervalEndToWorkDistanceIntervalBegin = 7,
        WorkTimeIntervalEndToRestIntervalBegin = 8,
        WorkDistanceIntervalEndtoRestEntervalBegin = 9,
        WorkoutEnd = 10,
        WorkoutTerminate = 11,
        WorkoutLogged = 12,
        WorkoutRearm = 13
    }
}
