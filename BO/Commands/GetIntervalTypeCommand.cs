using BO.Enums;
using BO.Interfaces;

namespace BO.Commands
{
    public class GetIntervalTypeCommand: ShortGetCommand
    {
        public override byte Code => (byte) PM3Command.GET_INTERVALTYPE;
        public override ushort? ResponseSize => 1;
        public override bool IsProprietary => true;

        public GetIntervalTypeCommand() : base(null)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = (IntervalType?)responseReader.ReadByte();
        }
    }
}
