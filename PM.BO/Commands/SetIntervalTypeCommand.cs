
using PM.BO.Enums;
using System;

namespace PM.BO.Commands
{
    /// <summary>
    /// Sets workout type
    /// </summary>
    public class SetIntervalTypeCommand : LongSetCommand
    {
        public override byte Code => (byte) PM3Command.SET_INTERVALTYPE;
        public override uint? ProprietaryWrapper => (uint)CSAFECommand.SET_PMCFG;

        public SetIntervalTypeCommand(uint[] data) : base(data)
        {

        }

        public SetIntervalTypeCommand(IntervalType intervalType)
        {
            Data = new uint[] { (uint)intervalType };
        }
    }
}
