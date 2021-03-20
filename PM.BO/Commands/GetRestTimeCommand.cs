using PM.BO.Enums;
using PM.BO.Interfaces;
using System;

namespace PM.BO.Commands
{
    public class GetRestTimeCommand: ShortGetCommand
    {
        public override byte Code => (byte) PM3Command.GET_RESTTIME;
        public override ushort? ResponseSize => 2;
        
        public override uint? Wrapper => (uint)CSAFECommand.SET_USERCFG1;
        private const ushort _refreshRate = 10;

        public GetRestTimeCommand() : base(_refreshRate)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = (ushort?) responseReader.ReadUShort();
        }
    }
}
