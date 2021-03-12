
using BO.Enums;

namespace BO.Commands
{
    public class SetTimeOfDayCommand : ShortSetCommand
    {
        public override byte Code => (byte) CSAFECommand.SET_TIME;
        public override ushort Size => 3;
        public override bool IsProprietary => false;

        public SetTimeOfDayCommand(uint[] data) : base(data)
        {

        }
    }
}
