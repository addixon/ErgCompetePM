
using PM.BO.Enums;
using System;

namespace PM.BO.Commands
{
    /// <summary>
    /// Sets the workout time goal
    /// </summary>
    public class SetWorkoutTimeCommand : LongSetCommand
    {
        public override byte Code => (byte) CSAFECommand.SET_TWORK;
        public override ushort Size => 2;
        public override bool IsProprietary => false;

        public SetWorkoutTimeCommand(uint[] data) : base(data)
        {

        }

        public SetWorkoutTimeCommand(int hours, int minutes, int seconds)
        {
            Data = new uint[]
            {
                BitConverter.GetBytes(hours)[0],
                BitConverter.GetBytes(minutes)[0],
                BitConverter.GetBytes(seconds)[0]
            };
        }
    }
}
