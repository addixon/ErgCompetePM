
using BO.Enums;

namespace BO.Commands
{
    public class SetHorizontalDistanceCommand : ShortSetCommand
    {
        public override byte Code => (byte) CSAFECommand.SET_HORIZONTAL;
        public override ushort Size => 2;
        
        public override bool IsProprietary => false;

        public SetHorizontalDistanceCommand(uint[] data) : base(data)
        {

        }
    }
}
