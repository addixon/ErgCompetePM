using PM.BO.Enums;
using PM.BO.Interfaces;
using System;

namespace PM.BO
{
    public class PMData : IErgData
    {
        public ushort? AccumulatedCalories { get; set; }
        public ushort? Cadence { get; set; }
        public byte? CurrentHeartRate { get; set; }
        public byte? DragFactor { get; set; }
        public ushort[]? ForcePlotData { get; set; }
        public ushort[]? HeartBeatData { get; set; }
        public ushort? HorizontalDistance { get; set; }
        public IntervalType? IntervalType { get; set; }
        public ushort? Pace { get; set; }
        public ushort? Power { get; set; }
        public ushort? RestTime { get; set; }
        public StrokeState? StrokeState { get; set; }
        public StrokeStatistics? StrokeStatistics { get; set; }
        public decimal? WorkDistance { get; set; }
        public TimeSpan? WorkoutDuration { get; set; }
        public byte? WorkoutIntervalCount { get; set; }
        public WorkoutState? WorkoutState { get; set; }
        public WorkoutType? WorkoutType { get; set; }
        public decimal? WorkTime { get; set; }
    }
}
