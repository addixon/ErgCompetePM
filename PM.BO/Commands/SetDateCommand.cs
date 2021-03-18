
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

        public SetDateCommand(uint[] data) : base(data)
        {

        }

        public SetDateCommand(int year, int month, int day)
        {
            Data = new uint[]
            {
                BitConverter.GetBytes((ushort)year)[1],
                BitConverter.GetBytes((ushort)month)[1],
                BitConverter.GetBytes((ushort)day)[1]
            };
        }
    }
}
