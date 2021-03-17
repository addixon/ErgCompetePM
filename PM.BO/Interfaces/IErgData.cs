using PM.BO.Enums;
using System;

namespace PM.BO.Interfaces
{
    public interface IErgData
    {
        ushort? AccumulatedCalories { get; set; }
        ushort? Cadence { get; set; }
        byte? CurrentHeartRate { get; set; }
        byte? DragFactor { get; set; }
        ushort[]? ForcePlotData { get; set; }
        ushort[]? HeartBeatData { get; set; }
        ushort? HorizontalDistance { get; set; }
        IntervalType? IntervalType { get; set; }
        ushort? Pace { get; set; }
        ushort? Power { get; set; }
        ushort? RestTime { get; set; }
        StrokeState? StrokeState { get; set; }
        StrokeStatistics? StrokeStatistics { get; set; }
        decimal? WorkDistance { get; set; }
        TimeSpan? WorkoutDuration { get; set; }
        byte? WorkoutIntervalCount { get; set; }
        WorkoutState? WorkoutState { get; set; }
        WorkoutType? WorkoutType { get; set; }
        decimal? WorkTime { get; set; }
    }
}
