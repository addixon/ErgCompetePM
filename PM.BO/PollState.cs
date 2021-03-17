using PM.BO.Enums;
using System;
using System.Collections.Generic;

namespace PM.BO
{
    public class PollState
    {
        public DateTime ExecutionStartTime { get; set; }
        public IEnumerable<PollInterval>? PollIntervals { get; set; }
        public ushort Iterations { get; set; }
        public Location Location { get; }

        public PollState(Location location)
        {
            Location = location;
        }
    }
}
