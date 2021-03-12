
using BO.Enums;

namespace BO.Commands
{
    public class SetProgramCommand : ShortSetCommand
    {
        public override byte Code => (byte) CSAFECommand.SET_PROGRAM;
        public override ushort Size => 2;
        
        public override bool IsProprietary => false;

        public SetProgramCommand(uint[] data) : base(data)
        {

        }
    }
}
