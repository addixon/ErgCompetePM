using PM.BO.Interfaces;
using System.Collections.Generic;

namespace PM.BO
{
    public record Workout : IWorkout
    {
        public IEnumerable<IInterval> Intervals { get; }

        public Workout(IEnumerable<IInterval> intervals)
        {
            Intervals = intervals;
        }
    }
}
