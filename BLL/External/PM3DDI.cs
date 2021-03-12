using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace BLL.External
{
    public class PM3DDI
    {
        /// <summary>
        /// Reads the serial number information from the PM
        /// </summary>
        [DllImport(@"DLLs/PM3DDICP.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "tkcmdsetDDI_serial_number")]
        public static extern short DDI_SerialNumber([In] ushort unit_address, [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder ser_ptr, [In] byte ser_len);

        /// <summary>
        /// Reads loader ifmrware version information from the PM
        /// </summary>
        /// <remarks>
        /// SUPPORTED ONLY BY PM3 VERSIONS 95 OR GREATER
        /// </remarks>
        [DllImport(@"DLLs/PM3DDICP.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "tkcmdsetDDI_loader_fw_version")]
        public static extern ushort DDI_LoaderFirmwareVersion([In] ushort port, [In] ushort ver_len, [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder ver_ptr);

        /// <summary>
        /// Reads the hardware version information from the PM
        /// </summary>
        [DllImport(@"DLLs/PM3DDICP.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "tkcmdsetDDI_hw_version")]
        public static extern ushort DDI_HardwareVersion([In] ushort unit_address, [In] ushort ver_len, [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder ver_ptr);

        /// <summary>
        /// Reads the firmware version information from the PM
        /// </summary>
        [DllImport(@"DLLs/PM3DDICP.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "tkcmdsetDDI_fw_version")]
        public static extern ushort DDI_FirmwareVersion([In] ushort unit_address, [In] ushort ver_len, [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder ver_ptr);

        /// <summary>
        /// Performs special operations based on the command
        /// </summary>
        [DllImport(@"DLLs/PM3DDICP.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "tkcmdsetDDI_special")]
        public static extern ushort DDI_Special([In] ushort unit_address, [In] ushort cmd, [In] uint in_data, [Out, MarshalAs(UnmanagedType.LPUTF8Str)] StringBuilder out_data);

        /// <summary>
        /// Reads status information from the PM
        /// </summary>
        [DllImport(@"DLLs/PM3DDICP.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "tkcmdsetDDI_status")]
        public static extern ushort DDI_Status([In] ushort port);

        /// <summary>
        /// Initializes the DLL error code interface and media interfaces
        /// </summary>
        [DllImport(@"DLLs/PM3DDICP.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "tkcmdsetDDI_init")]
        public static extern short DDI_Initialize();

        /// <summary>
        /// Discover all PM devices connected to the PC
        /// </summary>
        [DllImport(@"DLLs/PM3DDICP.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "tkcmdsetDDI_discover_pm3s")]
        public static extern short DDI_Discover([In] string product_name, [In] ushort starting_address, [Out] out ushort num_units);

        /// <summary>
        /// Discover all PM devices connected to the PC
        /// </summary>
        [DllImport(@"DLLs/PM3DDICP.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "tkcmdsetDDI_find_devices")]
        public static extern ushort DDI_FindDevices([In] string product_name, [Out] out ushort num_found, [Out] out ushort[] port_list);

        /// <summary>
        /// Shuts down the Command Set Toolkit functions on the specified port
        /// </summary>
        [DllImport(@"DLLs/PM3DDICP.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "tkcmdsetDDI_shutdown")]
        public static extern ushort DDI_Shutdown([In] ushort port);

        /// <summary>
        /// Shuts down the Command Set Toolkit functions on all open ports
        /// </summary>
        [DllImport(@"DLLs/PM3DDICP.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "tkcmdsetDDI_shutdown_all")]
        public static extern ushort DDI_Shutdown();
    }
}
