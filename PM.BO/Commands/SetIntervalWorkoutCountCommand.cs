using PM.BO.Enums;

namespace PM.BO.Commands
{
    /// <summary>
    /// Sets the current number of the interval workout
    /// </summary>
    public class SetIntervalWorkoutCountCommand : LongSetCommand
    {
        public override byte Code => (byte) PM3Command.SET_INTERVALWORKOUTCOUNT;
        public override uint? ProprietaryWrapper => (uint)CSAFECommand.SET_PMCFG;

        public SetIntervalWorkoutCountCommand(uint[] data) : base(data)
        {

        }

        public SetIntervalWorkoutCountCommand(int intervalNumber)
        {
            Data = new uint[] { (uint)intervalNumber };
        }
    }
}
