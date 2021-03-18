using PM.BO.Enums;
using PM.BO.Interfaces;
using System;

namespace PM.BO.Commands
{
    public class GetUserIdNumberCommand: ShortGetCommand
    {
        public override byte Code => (byte) CSAFECommand.GET_ID;
        public override ushort? ResponseSize => PMSettings.NumberOfIdDigits;
        
        

        public GetUserIdNumberCommand() : base(null)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort size)
        {
            Value = (ulong?) responseReader.ReadBytes(size);
        }
    }
}
