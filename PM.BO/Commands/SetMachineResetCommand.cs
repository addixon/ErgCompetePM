using PM.BO.Enums;

namespace PM.BO.Commands
{
    public class SetMachineResetCommand : ShortSetCommand
    {
        public override byte Code => (byte) CSAFECommand.RESET;

        public SetMachineResetCommand() : base()
        {

        }
    }
}
