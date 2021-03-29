using PM.BO.Enums;
using PM.BO.Interfaces;

namespace PM.BO.Commands
{
    public class GetBatteryLevelPercentCommand : ShortGetCommand
    {
        public override byte Code => (byte) PM3Command.GET_BATTERYLEVELPERCENT;
        public override ushort? ResponseSize => 1;
        
        public override uint? Wrapper => (uint)CSAFECommand.SET_USERCFG1;
        private const ushort _refreshRate = 2;


        public GetBatteryLevelPercentCommand() : base(_refreshRate)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = (uint?)responseReader.ReadByte();
        }
    }
}
