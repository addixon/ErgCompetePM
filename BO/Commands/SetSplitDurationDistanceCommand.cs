using BO.Enums;
using System.Linq;

namespace BO.Commands
{
    public class SetSplitDurationDistanceCommand : ShortSetCommand
    {
        public override byte Code => (byte) PM3Command.SET_SPLITDURATION;
        public override ushort Size => 5;
        
        public override bool IsProprietary => true;

        // Distance prefix
        private static readonly uint[] _prefix = { 128 };

        public SetSplitDurationDistanceCommand(uint[] data) : base(_prefix.Concat(data).ToArray())
        {

        }
    }
}
