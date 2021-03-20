using PM.BO.Enums;
using System;
using System.Linq;

namespace PM.BO.Commands
{
    /// <summary>
    /// Sets the distance duration of a split
    /// </summary>
    public class SetSplitDurationDistanceCommand : LongSetCommand
    {
        public override byte Code => (byte) PM3Command.SET_SPLITDURATION;
        public override uint? Wrapper => (uint)CSAFECommand.SET_PMCFG;

        public new static string Units => "Meters";
        public new static string Resolution => "1 Meter";

        /// <summary>
        /// Distance prefix
        /// </summary>
        /// <remarks>
        /// Used to specify distance (versus time) for the command
        /// </remarks>
        private static readonly uint[] _prefix = { 128 };

        public SetSplitDurationDistanceCommand(uint[] data) : base(_prefix.Concat(data).ToArray())
        {

        }

        public SetSplitDurationDistanceCommand(int meters)
        {
            byte[] bytes = BitConverter.GetBytes(meters).Reverse().ToArray();

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
