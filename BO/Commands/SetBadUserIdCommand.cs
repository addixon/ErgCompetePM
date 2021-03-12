using BO.Enums;

namespace BO.Commands
{
    public class SetBadUserIdCommand : ShortSetCommand
    {
        public override byte Code => (byte) CSAFECommand.BADID;
        public override ushort Size => 0;
        
        public override bool IsProprietary => false;

        public SetBadUserIdCommand(uint[] data) : base(data)
        {

        }
    }
}
