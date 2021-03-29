using PM.BO.Enums;
using System;

namespace PM.BO.Commands
{
    /// <summary>
    /// Sets workout type
    /// </summary>
    /// <remarks>
    /// TODO: This will eventually need to support more screen states than the Workout category
    /// </remarks>
    public class SetScreenStateCommand : LongSetCommand
    {
        public override byte Code => (byte) PM3Command.SET_SCREENSTATE;
        public override uint? Wrapper => (uint)CSAFECommand.SET_PMCFG;

        public SetScreenStateCommand(uint[] data) : base(data)
        {

        }

        public SetScreenStateCommand(ScreenState screenState)
        {
            if (screenState == null)
            {
                throw new ArgumentNullException(nameof(screenState));
            }

            if (screenState.ScreenType == null)
            {
                throw new ArgumentException("Screen type must not be null", nameof(screenState));
            }

            if (screenState.ScreenValue == null)
            {
                throw new ArgumentException("Screen Value must not be null", nameof(screenState));
            }

            Data = new uint[] { (uint)screenState.ScreenType, screenState.ScreenValue.Value };
        }

        public SetScreenStateCommand(ScreenType? screenType, ScreenValueWorkout? screenValueWorkout)
        {
            if (screenType == null)
            {
                throw new ArgumentNullException(nameof(screenType));
            }

            if (screenValueWorkout == null)
            {
                throw new ArgumentNullException(nameof(screenValueWorkout));
            }

            Data = new uint[] { (uint)screenType, (uint)screenValueWorkout };
        }
    }
}
