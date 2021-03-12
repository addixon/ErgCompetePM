using System.Runtime.InteropServices;

namespace BLL.External
{
    public class PM3CSAFE
    {
        /// <summary>
        /// Sends a CSAFE command to a PM device and returns the response data
        /// </summary>
        [DllImport(@"DLLs/PM3CsafeCP.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "tkcmdsetCSAFE_command")]
        public static extern short CSAFE_Command([In] ushort unit_address, [In] ushort cmd_data_size, [In] uint[] cmd_data, [In, Out] ref ushort rsp_data_size, [In] uint[] rsp_data);

        /// <summary>
        /// Initializes the DLL error code interface and configures the CSAFE protocol
        /// </summary>
        [DllImport(@"DLLs/PM3CsafeCP.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "tkcmdsetCSAFE_init_protocol")]
        public static extern ushort CSAFE_InitializeProtocol([In] ushort timeout = 1000);
    }
}
