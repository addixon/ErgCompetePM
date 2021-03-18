using PM.BO.Enums;
using PM.BO.Interfaces;

namespace PM.BO.Commands
{
    public class GetCurrentHeartRateCommand: ShortGetCommand
    {
        public override byte Code => (byte) CSAFECommand.GET_HRCUR;
        public override ushort? ResponseSize => 1;
        
        

        private const ushort _refreshRate = 1;

        public new string Units = "Beats/Min";
        public new string Resolution = "1 beat";

        public GetCurrentHeartRateCommand() : base(_refreshRate)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = (byte?) responseReader.ReadByte();
        }
    }
}
