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
        public (int BusNumber, int Address) Location { get; }

        public PollState((int BusNumber, int Address) location)
        {
            Location = location;
        }
    }
}
