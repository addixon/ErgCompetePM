using BO.Enums;
using System.Linq;

namespace BO.Commands
{
    public class SetSplitDurationTimeCommand : ShortSetCommand
    {
        public override byte Code => (byte) PM3Command.SET_SPLITDURATION;
        public override ushort Size => 5;
        
        public override bool IsProprietary => true;

        // Time prefix
        private static readonly uint[] _prefix = { 1 };

        public SetSplitDurationTimeCommand(uint[] data) : base(_prefix.Concat(data).ToArray())
        {
        }
    }
}
