﻿using PM.BO.Enums;

namespace PM.BO.Commands
{
    /// <summary>
    /// Configures workout
    /// </summary>
    /// <remarks>
    /// This is not defined in the api as a set command, but TODO: Create a new class of commands
    /// </remarks>
    public class SetConfigureWorkoutCommand : LongSetCommand
    {
        public override byte Code => (byte) PM3Command.CONFIGURE_WORKOUT;
        public override ushort Size => 0;
        public override bool IsProprietary => true;

        public SetConfigureWorkoutCommand(uint[] data) : base(data)
        {
        }

        public SetConfigureWorkoutCommand(WorkoutProgrammingMode programmingMode)
        {
            Data = new uint[] { (uint)programmingMode };
        }
    }
}
