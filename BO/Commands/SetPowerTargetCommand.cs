using PM.BO.Enums;
using System;

namespace PM.BO.Commands
{
    /// <summary>
    /// Sets the power goal
    /// </summary>
    public class SetPowerTargetCommand : LongSetCommand
    {
        public override byte Code => (byte) CSAFECommand.SET_POWER;
        public override ushort Size => 3;
        public override bool IsProprietary => false;

        public SetPowerTargetCommand(uint[] data) : base(data)
        {

        }

        public SetPowerTargetCommand(int watts)
        {
            byte[] bytes = BitConverter.GetBytes(watts);

            Data = new uint[]
            {
                bytes[0],
                bytes[1],
                (uint) UnitType.Watts
            };
        }
    }
}
