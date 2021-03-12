using BO.Enums;

namespace BO.Commands
{
    public class SetMachineStateHaveIdCommand : ShortSetCommand
    {
        public override byte Code => (byte) CSAFECommand.GOHAVEID;
        public override ushort Size => 0;
        
        public override bool IsProprietary => false;

        public SetMachineStateHaveIdCommand() : base(EmptyByteArray)
        {

        }
    }
}
