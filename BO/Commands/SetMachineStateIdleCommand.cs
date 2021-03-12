using BO.Enums;

namespace BO.Commands
{
    public class SetMachineStateIdleCommand : ShortSetCommand
    {
        public override byte Code => (byte) CSAFECommand.GOIDLE;
        public override ushort Size => 0;
        
        public override bool IsProprietary => false;

        public SetMachineStateIdleCommand() : base(EmptyByteArray)
        {

        }
    }
}
