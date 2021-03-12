
using BO.Enums;

namespace BO.Commands
{
    public class SetWorkoutTimeCommand : ShortSetCommand
    {
        public override byte Code => (byte) CSAFECommand.SET_TWORK;
        public override ushort Size => 2;
        
        public override bool IsProprietary => false;

        public SetWorkoutTimeCommand(uint[] data) : base(data)
        {

        }
    }
}
