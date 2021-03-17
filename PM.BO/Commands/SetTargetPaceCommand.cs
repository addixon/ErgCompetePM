using PM.BO.Enums;
using System;
using System.Linq;

namespace PM.BO.Commands
{
    /// <summary>
    /// Sets the rest duration
    /// </summary>
    public class SetTargetPaceCommand : LongSetCommand
    {
        public override byte Code => (byte) PM3Command.SET_TARGETPACE;
        public override ushort Size => 2;
        public override bool IsProprietary => true;

        public SetTargetPaceCommand(uint[] data) : base(data)
        {
        }

        public SetTargetPaceCommand(int targetPaceInSeconds)
        {
            Data = BitConverter.GetBytes(targetPaceInSeconds * 100).Select(b => (uint)b);
        }
    }
}
