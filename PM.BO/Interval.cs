using PM.BO.Enums;

namespace PM.BO
{
    public class Interval
    {
        /// <summary>
        /// Workout type
        /// </summary>
        public WorkoutType? WorkoutType { get; set; }

        /// <summary>
        /// The interval type
        /// </summary>
        /// <remarks>
        /// Required if a variable workout type is provided
        /// </remarks>
        public IntervalType? IntervalType { get; set; }

        /// <summary>
        /// Duration in seconds, meters, or calories
        /// </summary>
        public int? Duration { get; set; }

        /// <summary>
        /// Splits in seconds, meters, or calories
        /// </summary>
        /// <remarks>
        /// Required if a workout with splits is provided
        /// </remarks>
        public int? Splits { get; set; }

        /// <summary>
        /// Seconds of rest
        /// </summary>
        /// <remarks>
        /// Required if a workout with interval is provided
        /// </remarks>
        public int? SecondsOfRest { get; set; }

        /// <summary>
        /// Power goal in watts
        /// </summary>
        public int? PowerGoal { get; set; }

        /// <summary>
        /// Target pace in seconds
        /// </summary>
        public int? TargetPace { get; set; }
    }
}
