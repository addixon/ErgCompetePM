using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.BO.Enums
{
    public enum FrameCommands
    {
        STANDARD_START_FLAG = 0xF1,
        EXTENDED_START_FLAG = 0xF0,
        STOP_FRAME_FLAG = 0xF2,
        BYTE_STUFFING_FLAG = 0xF3
    }
}
