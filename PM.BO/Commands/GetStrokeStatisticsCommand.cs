using PM.BO.Enums;
using PM.BO.Interfaces;

namespace PM.BO.Commands
{
    public class GetStrokeStatisticsCommand: LongGetCommand
    {
        public override byte Code => (byte) PM3Command.GET_STROKESTATS;
        public override ushort? ResponseSize => 16;

        private const ushort _refreshRate = 2;

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Data is Reserved; using 0 for now
        /// </remarks>
        public GetStrokeStatisticsCommand() : base(_refreshRate)
        {
            Data = new uint[] { 0 };
        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value =(StrokeStatistics?) new StrokeStatistics()
            {
                Distance = (ushort) responseReader.ReadUShort(),
                DriveTime = (byte) responseReader.ReadByte(),
                RecoveryTime = (ushort) responseReader.ReadUShort(),
                Length = (byte) responseReader.ReadByte(),
                Count = (ushort) responseReader.ReadUShort(),
                Peak = (ushort) responseReader.ReadUShort(),
                ImpulseForce = (ushort) responseReader.ReadUShort(),
                AverageForce = (ushort) responseReader.ReadUShort(),
                WorkPerStroke = (ushort) responseReader.ReadUShort()
            };
        }
    }
}
