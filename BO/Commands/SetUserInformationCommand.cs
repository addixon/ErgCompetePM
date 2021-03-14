
using PM.BO.Enums;

namespace PM.BO.Commands
{
    /// <summary>
    /// Not implemented
    /// </summary>
    public class SetUserInformationCommand : LongSetCommand
    {
        public override byte Code => (byte) CSAFECommand.SET_USERINFO;
        public override ushort Size => 5;
        
        public override bool IsProprietary => false;

        public SetUserInformationCommand(uint[] data) : base(data)
        {

        }
    }
}
