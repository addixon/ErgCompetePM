using PM.BO.Enums;

namespace PM.BO.Commands
{
    public class SetMachineStateFinishedCommand : ShortSetCommand
    {
        public override byte Code => (byte) CSAFECommand.GOFINISHED;
        public override ushort Size => 0;
        
        public override bool IsProprietary => false;

        public SetMachineStateFinishedCommand() : base()
        {

        }
    }
}
