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
        public override uint? ProprietaryWrapper => (uint)CSAFECommand.SET_PMCFG;

        public SetTargetPaceCommand(uint[] data) : base(data)
        {
        }

        public SetTargetPaceCommand(int targetPaceInSeconds)
        {
            Data = BitConverter.GetBytes(targetPaceInSeconds * 100).Reverse().Select(b => (uint)b);
        }
    }
}
