using PM.BO.Enums;

namespace PM.BO.Interfaces
{
    public interface IInterval
    {
        int? Duration { get; set; }
        IntervalType? IntervalType { get; set; }
        int? SecondsOfRest { get; set; }
        int? Splits { get; set; }
        int? TargetPace { get; set; }
        WorkoutType? WorkoutType { get; set; }
    }
}