
using PM.BO.Enums;

namespace PM.BO.Commands
{
    /// <summary>
    /// Not implemented
    /// </summary>
    public class SetUserInformationCommand : LongSetCommand
    {
        public override byte Code => (byte) CSAFECommand.SET_USERINFO;

        public SetUserInformationCommand(uint[] data) : base(data)
        {

        }
    }
}
