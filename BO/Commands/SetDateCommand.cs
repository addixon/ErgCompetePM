
using BO.Enums;

namespace BO.Commands
{
    public class SetDateCommand : ShortSetCommand
    {
        public override byte Code => (byte) CSAFECommand.SET_DATE;
        public override ushort Size => 3;
        
        public override bool IsProprietary => false;

        public SetDateCommand(uint[] data) : base(data)
        {

        }
    }
}
