
using PM.BO.Enums;

namespace PM.BO.Commands
{
    /// <summary>
    /// Sets programmed/pre-stored workouts
    /// </summary>
    public class SetProgramCommand : LongSetCommand
    {
        public override byte Code => (byte) CSAFECommand.SET_PROGRAM;

        public SetProgramCommand(uint[] data) : base(data)
        {

        }

        public SetProgramCommand(ProgrammedWorkout workout)
        {
            Data = new uint[] { (uint)workout, 0x00 };
        }
    }
}
