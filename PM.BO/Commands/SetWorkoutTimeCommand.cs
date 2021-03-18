
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

        public SetWorkoutTimeCommand(uint[] data) : base(data)
        {

        }

        public SetWorkoutTimeCommand(int hours, int minutes, int seconds)
        {
            Data = new uint[]
            {
                BitConverter.GetBytes((ushort)hours)[1],
                BitConverter.GetBytes((ushort)minutes)[1],
                BitConverter.GetBytes((ushort)seconds)[1]
            };
        }
    }
}
