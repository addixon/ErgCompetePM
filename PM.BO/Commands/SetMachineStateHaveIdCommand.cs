using PM.BO.Enums;

namespace PM.BO.Commands
{
    public class SetMachineStateHaveIdCommand : ShortSetCommand
    {
        public override byte Code => (byte) CSAFECommand.GOHAVEID;

        public SetMachineStateHaveIdCommand() : base()
        {

        }
    }
}
