
using BO.Enums;

namespace BO.Commands
{
    public class SetUserInformationCommand : ShortSetCommand
    {
        public override byte Code => (byte) CSAFECommand.SET_USERINFO;
        public override ushort Size => 5;
        
        public override bool IsProprietary => false;

        public SetUserInformationCommand(uint[] data) : base(data)
        {

        }
    }
}
