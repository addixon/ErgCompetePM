using PM.BO.Enums;
using PM.BO.Interfaces;
using System;

namespace PM.BO.Commands
{
    public class GetWorkTimeCommand : ShortGetCommand
    {
        public override byte Code => (byte) PM3Command.GET_WORKTIME;
        public override ushort? ResponseSize => 5;
        
        public override bool IsProprietary => true;

        private const ushort _refreshRate = 10;

        public new string Units = "Seconds";
        public new string Resolution = "0.01 sec";

        public GetWorkTimeCommand() : base(_refreshRate)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            uint wholeTime = responseReader.ReadUInt();
            byte fractionalTime = (byte) responseReader.ReadByte();

            Value = (decimal?) Convert.ToDecimal(wholeTime + (fractionalTime * 0.01));
        }
    }
}
