using PM.BO.Enums;
using System;
using System.Linq;

namespace PM.BO.Commands
{
    /// <summary>
    /// Sets the power goal
    /// </summary>
    public class SetPowerTargetCommand : LongSetCommand
    {
        public override byte Code => (byte) CSAFECommand.SET_POWER;

        public SetPowerTargetCommand(uint[] data) : base(data)
        {

        }

        public SetPowerTargetCommand(int watts)
        {
            byte[] bytes = BitConverter.GetBytes((ushort)watts).Reverse().ToArray();

            Data = new uint[]
            {
                bytes[0],
                bytes[1],
                (uint) UnitType.Watts
            };
        }
    }
}
