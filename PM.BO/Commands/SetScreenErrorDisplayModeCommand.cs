using PM.BO.Enums;

namespace PM.BO.Commands
{
    /// <summary>
    /// Sets the screen error display mode
    /// </summary>
    public class SetScreenErrorDisplayModeCommand : LongSetCommand
    {
        public override byte Code => (byte) PM3Command.SET_SCREENERRORMODE;
        public override uint? Wrapper => (uint)CSAFECommand.SET_PMCFG;

        public SetScreenErrorDisplayModeCommand(uint[] data) : base(data)
        {

        }

        public SetScreenErrorDisplayModeCommand(ScreenErrorDisplayMode errorMode)
        {
            Data = new uint[] { (uint) errorMode };
        }
    }
}
