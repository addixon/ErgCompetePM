using System.Collections.Generic;

namespace PM.BO.Interfaces
{
    /// <summary>
    /// A workout with one or more intervals
    /// </summary>
    public interface IWorkout
    {
        IEnumerable<IInterval> Intervals { get; }
    }
}
