
using PM.BO.Enums;
using System;
using System.Linq;

namespace PM.BO.Commands
{
    /// <summary>
    /// Sets the timeout period for exiting certain states
    /// </summary>
    public class SetStateTimeoutCommand : LongSetCommand
    {
        public override byte Code => (byte) CSAFECommand.SET_TIMEOUT;
        public override ushort Size => 1;
        public override bool IsProprietary => false;

        public new static string Units => "Seconds";
        public new static string Resolution => "1 Second";

        public SetStateTimeoutCommand(uint[] data) : base(data)
        {

        }

        public SetStateTimeoutCommand(int seconds)
        {
            Data = BitConverter.GetBytes(seconds).Take(1).Select(b => (uint)b);
        }
    }
}
