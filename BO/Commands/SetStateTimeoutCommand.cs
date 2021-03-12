
using BO.Enums;

namespace BO.Commands
{
    public class SetStateTimeoutCommand : ShortSetCommand
    {
        public override byte Code => (byte) CSAFECommand.SET_TIMEOUT;
        public override ushort Size => 1;
        
        public override bool IsProprietary => false;

        public SetStateTimeoutCommand(uint[] data) : base(data)
        {

        }
    }
}
