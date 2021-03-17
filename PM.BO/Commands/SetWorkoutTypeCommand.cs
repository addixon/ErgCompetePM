
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
        public override ushort Size => 1;
        public override bool IsProprietary => true;

        public SetWorkoutTypeCommand(uint[] data) : base(data)
        {

        }

        public SetWorkoutTypeCommand(WorkoutType workoutType)
        {
            Data = new uint[] { (uint)workoutType };
        }
    }
}
