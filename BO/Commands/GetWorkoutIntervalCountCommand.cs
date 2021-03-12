using BO.Enums;
using BO.Interfaces;
using System;

namespace BO.Commands
{
    public class GetWorkoutIntervalCountCommand: ShortGetCommand
    {
        public override byte Code => (byte) PM3Command.GET_WORKOUTINTERVALCOUNT;
        public override ushort? ResponseSize => 1;
        
        public override bool IsProprietary => true;


        public GetWorkoutIntervalCountCommand() : base(null)
        {

        }

        protected override void ReadImplementation(IResponseReader responseReader, ushort _)
        {
            Value = (byte?) responseReader.ReadByte();
        }
    }
}
