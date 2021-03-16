using PM.BO.Enums;

namespace PM.BO.Commands
{
    /// <summary>
    /// Sets the PM to "Ready" state
    /// </summary>
    public class SetMachineStateReadyCommand : ShortSetCommand
    {
        public override byte Code => (byte) CSAFECommand.GOREADY;
        public override ushort Size => 0;
        
        public override bool IsProprietary => false;

        public SetMachineStateReadyCommand() : base()
        {

        }
    }
}
