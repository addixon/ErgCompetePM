using BO.Enums;

namespace BO.Commands
{
    public class SetMachineResetCommand : ShortSetCommand
    {
        public override byte Code => (byte) CSAFECommand.RESET;
        public override ushort Size => 0;
        
        public override bool IsProprietary => false;

        public SetMachineResetCommand() : base(EmptyByteArray)
        {

        }
    }
}
