using PM.BO.Enums;

namespace PM.BO.Commands
{
    public class SetMachineStateIdleCommand : ShortSetCommand
    {
        public override byte Code => (byte) CSAFECommand.GOIDLE;        

        public SetMachineStateIdleCommand() : base()
        {

        }
    }
}
