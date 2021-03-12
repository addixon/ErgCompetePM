using BO.Enums;

namespace BO.Commands
{
    public class SetUserIdDigitsCommand : ShortSetCommand
    {
        public override byte Code => (byte) CSAFECommand.IDDIGITS;
        public override ushort Size => 1;
        
        public override bool IsProprietary => false;

        public SetUserIdDigitsCommand(uint[] data) : base(data)
        {

        }
    }
}
