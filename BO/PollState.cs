using BO.Enums;
using System;
using System.Collections.Generic;

namespace BO
{
    public class PollState
    {
        public DateTime ExecutionStartTime { get; set; }
        public ushort Port { get; set; }
        public IEnumerable<PollInterval>? PollIntervals { get; set; }
        public ushort Iterations { get; set; }
    }
}
