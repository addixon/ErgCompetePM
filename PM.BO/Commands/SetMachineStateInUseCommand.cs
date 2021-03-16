using PM.BO.Enums;

namespace PM.BO.Commands
{
    public class SetMachineStateInUseCommand : ShortSetCommand
    {
        public override byte Code => (byte) CSAFECommand.GOINUSE;
        public override ushort Size => 0;
        
        public override bool IsProprietary => false;

        public SetMachineStateInUseCommand() : base()
        {

        }
    }
}
