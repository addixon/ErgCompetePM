using BO.Enums;

namespace BO.Commands
{
    public class SetMachineStateReadyCommand : ShortSetCommand
    {
        public override byte Code => (byte) CSAFECommand.GOREADY;
        public override ushort Size => 0;
        
        public override bool IsProprietary => false;

        public SetMachineStateReadyCommand() : base(EmptyByteArray)
        {

        }
    }
}
