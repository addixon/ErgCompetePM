using BO.Enums;

namespace BO.Commands
{
    public class SetMachineStateFinishedCommand : ShortSetCommand
    {
        public override byte Code => (byte) CSAFECommand.GOFINISHED;
        public override ushort Size => 0;
        
        public override bool IsProprietary => false;

        public SetMachineStateFinishedCommand() : base(EmptyByteArray)
        {

        }
    }
}
