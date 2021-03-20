using PM.BO.Enums;
using System.Collections.Generic;

namespace PM.BO.Commands
{
    /// <summary>
    /// Sets the current number of the interval workout
    /// </summary>
    public class SetWorkoutIntervalCountCommand : LongSetCommand
    {
        public override byte Code => (byte) PM3Command.SET_INTERVALWORKOUTCOUNT;
        public override uint? Wrapper => (uint)CSAFECommand.SET_PMCFG;
        public override IEnumerable<uint>? ParentTo => new uint[]
        {
            (uint)PM3Command.SET_WORKOUTTYPE,
            (uint)PM3Command.SET_INTERVALTYPE,
            (uint)PM3Command.SET_WORKOUTDURATION,
            (uint)PM3Command.SET_RESTDURATION,
            (uint)PM3Command.SET_TARGETPACE,
            (uint)PM3Command.CONFIGURE_WORKOUT
        };

        public SetWorkoutIntervalCountCommand(uint[] data) : base(data)
        {

        }

        public SetWorkoutIntervalCountCommand(int intervalNumber)
        {
            Data = new uint[] { (uint)intervalNumber };
        }
    }
}
