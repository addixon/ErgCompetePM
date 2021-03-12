using BO.Enums;

namespace BO.Commands
{
    public class SetScreenErrorDisplayModeCommand : ShortSetCommand
    {
        public override byte Code => (byte) PM3Command.SET_SCREENERRORMODE;
        public override ushort Size => 1;
        
        public override bool IsProprietary => true;

        public SetScreenErrorDisplayModeCommand(uint[] data) : base(data)
        {

        }
    }
}
