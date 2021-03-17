using PM.BO.Enums;

namespace PM.BO
{
    public struct WorkoutDetails
    {
        public WorkoutDuration WorkoutDuration { get; }
        public bool RequireRest { get; }
        public bool RequireSplits { get; }

        public WorkoutDetails(WorkoutDuration workoutDuration, bool requireRest, bool requireSplits)
        {
            WorkoutDuration = workoutDuration;
            RequireRest = requireRest;
            RequireSplits = requireSplits;
        }
    }
}
