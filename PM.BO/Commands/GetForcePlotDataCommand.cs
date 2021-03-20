using PM.BO.Enums;
using PM.BO.Interfaces;
using System.Collections.Generic;

namespace PM.BO.Commands
{
    public class GetForcePlotDataCommand: ShortGetCommand
    {
        public override byte Code => (byte) PM3Command.GET_FORCEPLOTDATA;
        public override ushort? ResponseSize => 33;
        
        public override uint? Wrapper => (uint)CSAFECommand.SET_USERCFG1;

        public GetForcePlotDataCommand() : base(null)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            byte size = (byte) responseReader.ReadByte();
            List<ushort> plots = new List<ushort>();

            for (int i = 0; i < size; i += 2)
            {
                plots.Add((ushort) responseReader.ReadUShort());
            }

            Value = (ushort[]?) plots.ToArray();
        }
    }
}
