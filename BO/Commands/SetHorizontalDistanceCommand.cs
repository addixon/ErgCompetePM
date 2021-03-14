
using PM.BO.Enums;
using System;

namespace PM.BO.Commands
{
    /// <summary>
    /// Sets the horizontal distance goal
    /// </summary>
    public class SetHorizontalDistanceCommand : LongSetCommand
    {
        public override byte Code => (byte) CSAFECommand.SET_HORIZONTAL;
        public override ushort Size => 3;
        public override bool IsProprietary => false;

        public SetHorizontalDistanceCommand(uint[] data) : base(data)
        {

        }

        public SetHorizontalDistanceCommand(int distance, UnitType units)
        {
            byte[] bytes = BitConverter.GetBytes(distance);

            Data = new uint[]
            {
                bytes[0],
                bytes[1],
                (uint) units
            };
        }
    }
}
