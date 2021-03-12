using BO.Enums;
using BO.Interfaces;
using System;

namespace BO.Commands
{
    public class GetUserIdNumberCommand: ShortGetCommand
    {
        public override byte Code => (byte) CSAFECommand.GET_ID;
        public override ushort? ResponseSize => PMSettings.NumberOfIdDigits;
        
        public override bool IsProprietary => false;

        public GetUserIdNumberCommand() : base(null)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort size)
        {
            Value = (ulong?) responseReader.ReadBytes(size);
        }
    }
}
