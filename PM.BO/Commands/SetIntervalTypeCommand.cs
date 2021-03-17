
using PM.BO.Enums;
using System;

namespace PM.BO.Commands
{
    /// <summary>
    /// Sets workout type
    /// </summary>
    public class SetIntervalTypeCommand : LongSetCommand
    {
        public override byte Code => (byte) PM3Command.SET_INTERVALTYPE;
        public override ushort Size => 1;
        public override bool IsProprietary => true;

        public SetIntervalTypeCommand(uint[] data) : base(data)
        {

        }

        public SetIntervalTypeCommand(IntervalType intervalType)
        {
            Data = new uint[] { (uint)intervalType };
        }
    }
}
