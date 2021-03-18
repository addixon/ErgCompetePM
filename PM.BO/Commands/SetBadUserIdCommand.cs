using PM.BO.Enums;

namespace PM.BO.Commands
{
    /// <summary>
    /// Sets the invalid user id
    /// </summary>
    public class SetBadUserIdCommand : ShortSetCommand
    {
        public override byte Code => (byte) CSAFECommand.BADID;

        public SetBadUserIdCommand() : base()
        {

        }
    }
}
