using PM.BO.Enums;
using PM.BO.Interfaces;
using System;

namespace PM.BO.Commands
{
    public class GetWorkoutDurationCommand: ShortGetCommand
    {
        public override byte Code => (byte) CSAFECommand.GET_TWORK;
        public override ushort? ResponseSize => 3;
        
        public override bool IsProprietary => false;
        private const ushort _refreshRate = 10;

        public GetWorkoutDurationCommand() : base(_refreshRate)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            byte hour = (byte) responseReader.ReadByte();
            byte minute = (byte) responseReader.ReadByte();
            byte second = (byte) responseReader.ReadByte();

            Value = (TimeSpan?) new TimeSpan(hour, minute, second);
        }
    }
}
