using PM.BO.Enums;
using System;
using System.Linq;

namespace PM.BO.Commands
{
    /// <summary>
    /// Sets the time duration of a split
    /// </summary>
    public class SetSplitDurationTimeCommand : LongSetCommand
    {
        public override byte Code => (byte) PM3Command.SET_SPLITDURATION;
        public override uint? ProprietaryWrapper => (uint)CSAFECommand.SET_PMCFG;

        public new static string Units => "Seconds";
        public new static string Resolution => "0.01 Seconds";

        /// <summary>
        /// Time prefix
        /// </summary>
        /// <remarks>
        /// Used to specify time (versus distance) for the command
        /// </remarks>
        private static readonly uint[] _prefix = { 1 };

        public SetSplitDurationTimeCommand(uint[] data) : base(_prefix.Concat(data).ToArray())
        {
        }

        public SetSplitDurationTimeCommand(int seconds)
        {
            byte[] bytes = BitConverter.GetBytes(seconds).Reverse().ToArray();

            Data = new uint[]
            {
                _prefix[0],
                bytes[0],
                bytes[1],
                bytes[2],
                bytes[3]
            };
        }
    }
}
