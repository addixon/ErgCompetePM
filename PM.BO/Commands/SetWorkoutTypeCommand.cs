
using PM.BO.Enums;
using System;

namespace PM.BO.Commands
{
    /// <summary>
    /// Sets workout type
    /// </summary>
    public class SetWorkoutTypeCommand : LongSetCommand
    {
        public override byte Code => (byte) PM3Command.SET_WORKOUTTYPE;
        public override uint? Wrapper => (uint)CSAFECommand.SET_PMCFG;
        public override int? MaximumOccurrenceInParent => 1;

        public SetWorkoutTypeCommand(uint[] data) : base(data)
        {

        }

        public SetWorkoutTypeCommand(WorkoutType workoutType)
        {
            Data = new uint[] { (uint)workoutType };
        }
    }
}
