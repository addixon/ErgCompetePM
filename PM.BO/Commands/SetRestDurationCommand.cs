using PM.BO.Enums;
using System;
using System.Linq;

namespace PM.BO.Commands
{
    /// <summary>
    /// Sets the rest duration
    /// </summary>
    public class SetRestDurationCommand : LongSetCommand
    {
        public override byte Code => (byte) PM3Command.SET_RESTDURATION;
        public override ushort Size => 2;
        public override bool IsProprietary => true;

        public SetRestDurationCommand(uint[] data) : base(data)
        {
        }

        public SetRestDurationCommand(int restDurationInSeconds)
        {
            Data = BitConverter.GetBytes((ushort)restDurationInSeconds * 100).Select(b => (uint)b);
        }
    }
}
