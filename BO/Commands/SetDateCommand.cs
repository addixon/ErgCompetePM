
using PM.BO.Enums;
using System;

namespace PM.BO.Commands
{
    /// <summary>
    /// Sets the date
    /// </summary>
    public class SetDateCommand : LongSetCommand
    {
        public override byte Code => (byte) CSAFECommand.SET_DATE;
        public override ushort Size => 3;
        public override bool IsProprietary => false;

        public SetDateCommand(uint[] data) : base(data)
        {

        }

        public SetDateCommand(int year, int month, int day)
        {
            Data = new uint[]
            {
                BitConverter.GetBytes(year)[0],
                BitConverter.GetBytes(month)[0],
                BitConverter.GetBytes(day)[0]
            };
        }
    }
}
