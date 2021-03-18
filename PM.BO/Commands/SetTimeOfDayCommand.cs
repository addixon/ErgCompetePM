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

        public SetTimeOfDayCommand(uint[] data) : base(data)
        {

        }

        public SetTimeOfDayCommand(int hour, int minute, int second) 
        {
            Data = new uint[]
            {
                BitConverter.GetBytes((ushort)hour)[1],
                BitConverter.GetBytes((ushort)minute)[0],
                BitConverter.GetBytes((ushort)second)[0]
            };
        }
    }
}
