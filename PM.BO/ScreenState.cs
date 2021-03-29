using PM.BO.Enums;
using System;

namespace PM.BO
{
    public class ScreenState
    {
        public ScreenType? ScreenType { get; set; }
        public uint? ScreenValue { get; set; }

        public Type GetScreenValueType()
        {
            if (ScreenType == null)
            {
                throw new InvalidOperationException("Screen type must be defined.");
            }

            return ScreenType switch
            {
                Enums.ScreenType.Workout => typeof(ScreenValueWorkout),
                _ => throw new NotSupportedException("Screen type is not yet supported."),
            };
        }
    }
}
