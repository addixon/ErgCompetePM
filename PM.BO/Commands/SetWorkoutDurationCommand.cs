using PM.BO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PM.BO.Commands
{
    /// <summary>
    /// Sets workout duration
    /// </summary>
    public class SetWorkoutDurationCommand : LongSetCommand
    {
        public override byte Code => (byte) PM3Command.SET_WORKOUTDURATION;
        public override ushort Size => 5;
        public override bool IsProprietary => true;

        public SetWorkoutDurationCommand(uint[] data) : base(data)
        {

        }

        public SetWorkoutDurationCommand(WorkoutDuration? workoutDuration, int duration)
        {
            if (workoutDuration == null)
            {
                throw new ArgumentNullException(nameof(workoutDuration));
            }

            List<uint> data = new() { (uint)workoutDuration };
            data.AddRange(BitConverter.GetBytes(duration).Select(b => (uint)b));

            Data = data;
        }
    }
}
