using PM.BO.Enums;
using PM.BO.Interfaces;
using System;

namespace PM.BO.Commands
{
    public class GetWorkoutIntervalCountCommand: ShortGetCommand
    {
        public override byte Code => (byte) PM3Command.GET_WORKOUTINTERVALCOUNT;
        public override ushort? ResponseSize => 1;
        
        public override uint? Wrapper => (uint)CSAFECommand.SET_USERCFG1;


        public GetWorkoutIntervalCountCommand() : base(null)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = (byte?) responseReader.ReadByte();
        }
    }
}
