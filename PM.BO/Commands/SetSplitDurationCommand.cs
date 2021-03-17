using PM.BO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PM.BO.Commands
{
    /// <summary>
    /// Sets the duration of a split
    /// </summary>
    public class SetSplitDurationCommand : LongSetCommand
    {
        public override byte Code => (byte) PM3Command.SET_SPLITDURATION;
        public override ushort Size => 5;
        public override bool IsProprietary => true;

        public SetSplitDurationCommand(uint[] data) : base(data)
        {
        }

        public SetSplitDurationCommand(WorkoutDuration workoutDuration, int duration)
        {
            List<uint> data = new() { (uint)workoutDuration };
            data.AddRange(BitConverter.GetBytes(duration).Select(b => (uint)b));

            Data = data;
        }
    }
}
