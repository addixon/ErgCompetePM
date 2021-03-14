using PM.BO.Enums;
using System;

namespace PM.BO.Commands
{
    /// <summary>
    /// Sets the time of day
    /// </summary>
    public class SetTimeOfDayCommand : LongSetCommand
    {
        public override byte Code => (byte) CSAFECommand.SET_TIME;
        public override ushort Size => 3;
        public override bool IsProprietary => false;

        public SetTimeOfDayCommand(uint[] data) : base(data)
        {

        }

        public SetTimeOfDayCommand(int hour, int minute, int second) 
        {
            Data = new uint[]
            {
                BitConverter.GetBytes(hour)[0],
                BitConverter.GetBytes(minute)[0],
                BitConverter.GetBytes(second)[0]
            };
        }
    }
}
