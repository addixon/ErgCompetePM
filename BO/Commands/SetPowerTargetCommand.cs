
using BO.Enums;

namespace BO.Commands
{
    public class SetPowerTargetCommand : ShortSetCommand
    {
        public override byte Code => (byte) CSAFECommand.SET_POWER;
        public override ushort Size => 2;
        
        public override bool IsProprietary => false;

        public SetPowerTargetCommand(uint[] data) : base(data)
        {

        }
    }
}
