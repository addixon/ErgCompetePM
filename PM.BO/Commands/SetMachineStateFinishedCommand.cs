using PM.BO.Enums;

namespace PM.BO.Commands
{
    public class SetMachineStateFinishedCommand : ShortSetCommand
    {
        public override byte Code => (byte) CSAFECommand.GOFINISHED;

        public SetMachineStateFinishedCommand() : base()
        {

        }
    }
}
