using PM.BO.Enums;
using PM.BO.Interfaces;
using System;

namespace PM.BO.Commands
{
    public class GetWorkDistanceCommand: ShortGetCommand
    {
        public override byte Code => (byte) PM3Command.GET_WORKDISTANCE;
        public override ushort? ResponseSize => 5;
        
        
        public override uint? Wrapper => (uint)CSAFECommand.SET_USERCFG1;
        private const ushort _refreshRate = 10;

        public new string Units = "Meters";
        public new string Resolution = "0.1 m";

        public GetWorkDistanceCommand() : base(_refreshRate)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            uint wholeTime = responseReader.ReadUInt();
            uint fractionalTime = (byte) responseReader.ReadByte();

            Value = (decimal?) Convert.ToDecimal(wholeTime + (fractionalTime * 0.01));
        }
    }
}
