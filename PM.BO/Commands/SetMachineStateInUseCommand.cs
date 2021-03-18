using PM.BO.Enums;

namespace PM.BO.Commands
{
    public class SetMachineStateInUseCommand : ShortSetCommand
    {
        public override byte Code => (byte) CSAFECommand.GOINUSE;        

        public SetMachineStateInUseCommand() : base()
        {

        }
    }
}
