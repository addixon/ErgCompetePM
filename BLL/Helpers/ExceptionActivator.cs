using BO.Exceptions;
using BO.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace BLL.Helpers
{
    public class ExceptionActivator : IExceptionActivator
    {
        private static readonly IDictionary<short, Type>? Exceptions = new Dictionary<short, Type>
            {
                #region Exceptions
                { -120, typeof(TKCMDPR_INVALID_MSG_TYPE) },
                { -121, typeof(TKCMDPR_INVALID_CMD_ERR) },
                { -122, typeof(TKCMDPR_INVALID_CMD_ADDR_ERR) },
                { -123, typeof(TKCMDPR_INVALID_DEST_ADDR_ERR) },
                { -124, typeof(TKCMDPR_INVALID_DEST_INTF_ERR) },
                { -125, typeof(TKCMDPR_INVALID_INTF_ERR) },
                { -126, typeof(TKCMDPR_ROUTE_TABLE_FULL_ERR) },
                { -127, typeof(TKCMDPR_NO_DATA_AVAILABLE_ERR) },
                { -130, typeof(TKDATALOG_INIT_ERR) },
                { -131, typeof(TKDATALOG_READ_ERR) },
                { -132, typeof(TKDATALOG_CARD_INIT_ERR) },
                { -133, typeof(TKDATALOG_MULTI_STRUCT_ERR) },
                { -134, typeof(TKDATALOG_WRITE_ERR) },
                { -135, typeof(TKDATALOG_RECORDIDENTIFIER_ERR) },
                { -140, typeof(TKDISP_INVALID_CHAR_ERR) },
                { -141, typeof(TKDISP_INVALIDPARAM_ERR) },
                { -142, typeof(TKDISP_STRING_TOO_LONG_ERR) },
                { -143, typeof(TKDISP_STRING_TOO_HIGH_ERR) },
                { -150, typeof(TKEEPROM_INIT_ERR) },
                { -151, typeof(TKEEPROM_ACK_ERR) },
                { -152, typeof(TKEEPROM_STOP_ERR) },
                { -153, typeof(TKEEPROM_INVALID_END_ADDR) },
                { -154, typeof(TKEEPROM_WRITE_TIMEOUT_ERR) },
                { -155, typeof(TKEEPROM_WRITE_READ_ERR) },
                { -156, typeof(TKEEPROM_WRITE_VERIFY_ERR) },
                { -157, typeof(TKEEPROM_CHKSM_READ_ERR) },
                { -160, typeof(TKFRAME_CSAFE_FRAME_STUFF_ERR) },
                { -161, typeof(TKFRAME_CSAFE_FRAME_CHKSM_ERR) },
                { -162, typeof(TKFRAME_NO_SCI_FRAME_ERR) },
                { -163, typeof(TKFRAME_NO_USB_FRAME_ERR) },
                { -164, typeof(TKFRAME_CSAFE_INVALID_SHORT_CMD_ERR) },
                { -165, typeof(TKFRAME_CSAFE_INVALID_LONG_CMD_ERR) },
                { -166, typeof(TKFRAME_CSAFE_NO_END_CHAR_ERR) },
                { -170, typeof(TKHDW_EVENT_BURST_STACK_OVF_ERR) },
                { -171, typeof(TKHDW_EVENT_BURST_STACK_UNF_ERR) },
                { -180, typeof(TKHRTMON_INVALID_NUM_MEAS_ERR) },
                { -181, typeof(TKHRTMON_TOO_FEW_MEAS_ERR) },
                { -190, typeof(INTERRUPT_TOOLKIT_ERR) },
                { -200, typeof(TKMEM_INVALID_MEMTYPE_ERR) },
                { -201, typeof(TKMEM_INVALID_START_ADDR_ERR) },
                { -202, typeof(TKMEM_INVALID_END_ADDR_ERR) },
                { -203, typeof(TKMEM_FLASH_WRITE_ERR) },
                { -210, typeof(TKRTTIMER_INVALID_MONTH_ERR) },
                { -211, typeof(TKRTTIMER_INVALID_DAY_ERR) },
                { -212, typeof(TKRTTIMER_INVALID_TIMER_NUM_ERR) },
                { -220, typeof(TKSCI_INVALID_PORT_ERR) },
                { -221, typeof(TKSCI_TX_SEND_ERR) },
                { -222, typeof(TKSCI_RX_TIMEOUT_ERR) },
                { -230, typeof(TKSCRN_INVALID_SPECFUNCTYPE) },
                { -240, typeof(TKSMCD_ACK_ERR) },
                { -241, typeof(TKSMCD_STOP_ERR) },
                { -242, typeof(TKSMCD_INVALID_END_ADDR) },
                { -243, typeof(TKSMCD_WRITE_TIMEOUT_ERR) },
                { -244, typeof(TKSMCD_WRITE_READ_ERR) },
                { -245, typeof(TKSMCD_WRITE_VERIFY_ERR) },
                { -246, typeof(TKSMCD_CHKSM_READ_ERR) },
                { -250, typeof(TKTACH_INVALID_NUM_MEAS_ERR) },
                { -251, typeof(TKTACH_TOO_FEW_MEAS_ERR) },
                { -260, typeof(TKTIME_INVALID_MONTH_ERR) },
                { -261, typeof(TKTIME_INVALID_DAY_ERR) },
                { -280, typeof(TKUSER_INIT_ERR) },
                { -281, typeof(TKUSER_PEN_INIT_ERR) },
                { -282, typeof(TKUSER_PEN_EVENT_ERR) },
                { -283, typeof(TKUSER_PEN_CALIB_START_ERR) },
                { -300, typeof(TKCRC_ERR) },
                { -310, typeof(TKCMDSET_UNKNOWN_SPECIAL_ERR) },
                { -320, typeof(TKCIPHER_NOT_BLOCK_MULT_ERR) },
                { -330, typeof(TKUSB_BAD_DESC_RQT_ERR) },
                { -331, typeof(TKUSB_INVALID_EPNUM_ERR) },
                { -332, typeof(TKUSB_RX_TIMEOUT_ERR) },
                { -333, typeof(TKUSB_EP_RX_OVERRUN_ERR) },
                { -334, typeof(TKUSB_INIT_EPNUM_ERR) },
                { -335, typeof(TKUSB_GET_RX_CHAR_ERR) },
                { -336, typeof(TKUSB_BUS_DISABLE_ERR) },
                { -337, typeof(TKUSB_BUS_RESET_ERR) },
                { -338, typeof(TKUSB_NO_FEATURE_REPORT_ERR) },
                { -339, typeof(TKUSB_INVALID_STRING_ID_ERR) },
                { -340, typeof(TKUSB_EP_TX_OVERRUN_ERR) },
                { -341, typeof(TKUSB_INVALID_TX_LEN_ERR) },
                { -500, typeof(TKDIAG_DIAGFAIL_ERR) },
                { -810, typeof(IOADCONV_BG_TIMEOUT_ERR) },
                { -811, typeof(IOADCONV_RESET_TIMEOUT_ERR) },
                { -812, typeof(IOADCONV_INVALID_CHAN_ERR) },
                { -813, typeof(IOADCONV_NOT_RDY_ERR) },
                { -814, typeof(IOADCONV_INVALID_REF_ERR) },
                { -815, typeof(IOADCONV_INIT_ADC_ERR) },
                { -820, typeof(IODMA_INVALID_MEM_CHAN_ERR) },
                { -821, typeof(IODMA_INVALID_IO_RQST_CHAN_ERR) },
                { -822, typeof(IODMA_INIT_DMA_ERR) },
                { -823, typeof(IODMA_QUEUE_FULL_ERR) },
                { -830, typeof(IOHDW_MEM_INVALID_CS_ERR) },
                { -831, typeof(IOHDW_INVALID_DMACLK_ERR) },
                { -832, typeof(IOHDW_INVALID_SYSCLK_ERR) },
                { -840, typeof(IOI2C_NOACK_ERR) },
                { -841, typeof(IOI2C_INIT_WDR_TIMOUT_ERR) },
                { -842, typeof(IOI2C_INIT_XMIT_TIMOUT_ERR) },
                { -843, typeof(IOI2C_SEND_XMIT_TIMOUT_ERR) },
                { -844, typeof(IOI2C_GET_RECV_TIMOUT_ERR) },
                { -845, typeof(IOI2C_STOP_TIMEOUT_ERR) },
                { -846, typeof(IOI2C_WDR_TIMOUT_ERR) },
                { -847, typeof(IOI2C_INVALID_BAUD) },
                { -850, typeof(INTERRUPT_PRIMITIVE_ERR) },
                { -860, typeof(IOLCD_DISPINIT_ERR) },
                { -861, typeof(IOLCD_INVALIDPARAM_ERR) },
                { -870, typeof(IOMEM_FLASH_ERASE_TIMEOUT_ERR) },
                { -871, typeof(IOMEM_FLASH_WRITE_TIMEOUT_ERR) },
                { -880, typeof(IORTCLOCK_WRITE_TIME_ERR) },
                { -890, typeof(IOSCI_INVALID_PORT_ERR) },
                { -891, typeof(IOSCI_INVALID_BAUD_ERR) },
                { -892, typeof(IOSCI_INVALID_CNT_ERR) },
                { -893, typeof(IOSCI_INIT_PORT_ERR) },
                { -900, typeof(SMARTCARD_PRIMITIVE_ERR) },
                { -910, typeof(IOTIMER_INVALID_TIMERID_ERR) },
                { -920, typeof(USER_PRIMITIVE_ERR) },
                { -930, typeof(IOUSB_RST_TIMOUT_ERR) },
                { -931, typeof(IOUSB_CFG_TIMOUT_ERR) },
                { -932, typeof(IOUSB_CFG_ENDPT_ERR) },
                { -933, typeof(IOUSB_SETUP_ERR) },
                { -934, typeof(IOUSB_FIFO_RD_ERR) },
                { -935, typeof(IOUSB_NULL_PTR_ERR) },
                { -936, typeof(IOUSB_BUS_INIT_ERR) },
                { -937, typeof(IOUSB_TX_BUFFER_ERR) },
                { -938, typeof(IOUSB_EP_BUSY_ERR) },
                { -939, typeof(IOUSB_EP_INVALID_ERR) },
                { -940, typeof(IOUSB_WAKEUP_DISABLE_ERR) },
                { -941, typeof(IOUSB_BAD_FRAMENUM_ERR) },
                { -942, typeof(IOUSB_CFG_DEV_ERR) },
                { -943, typeof(IOUSB_BAD_IFCNUM_ERR) },
                { -1000, typeof(TKEXP_RS232_INVALID_ERR) },
                { -1001, typeof(TKEXP_CF_NOTPRESENT_ERR) },
                { -1002, typeof(TKEXP_CF_CIRQINVALID_ERR) },
                { -1003, typeof(TKEXP_CF_CARDNOTREADY_ERR) },
                { -1004, typeof(TKEXP_CF_MEMTEST_ERR) },
                { -1005, typeof(TKEXP_CF_INVALIDSTATE_ERR) },
                { -1006, typeof(TKEXP_CF_RFVENDORSTRING_ERR) },
                { -2000, typeof(TKDEBUG_INIT_ERR) },
                { -10000, typeof(TKTIME_NO_HIRES_ERR) },
                { -10001, typeof(TKTIME_ABORT_ERR) },
                { -10100, typeof(TKUSB_INVALID_PORT_ERR) },
                { -10101, typeof(TKUSB_INVALID_DEVICE_NAME_ERR) },
                { -10102, typeof(TKUSB_WRITE_FAILED_ERR) },
                { -10103, typeof(TKUSB_WRITE_INCOMPLETE_ERR) },
                { -10104, typeof(TKUSB_WRITE_TIMEOUT_ERR) },
                { -10105, typeof(TKUSB_READ_FAILED_ERR) },
                { -10106, typeof(TKUSB_READ_INCOMPLETE_ERR) },
                { -10107, typeof(TKUSB_READ_TIMEOUT_ERR) },
                { -10108, typeof(TKUSB_DATA_NOT_AVAILABLE_ERR) },
                { -10109, typeof(TKUSB_NO_PORT_INIT_ERR) },
                { -10110, typeof(TKUSB_FLUSH_FAILED_ERR) },
                { -10111, typeof(TKUSB_SET_FEATURE_ERR) },
                { -10112, typeof(TKUSB_GET_STRING_ERR) },
                { -10150, typeof(TKDDI_INVALID_PORT_ERR) },
                { -10151, typeof(TKDDI_NOT_IMPLEMENTED_ERR) },
                { -10152, typeof(TKDDI_UNIT_ADDRESS_ERR) },
                { -10153, typeof(TKDDI_PROTOCOL_INIT_ERR) },
                { -10154, typeof(TKDDI_PROTOCOL_NOT_DEFINED_ERR) },
                { -10155, typeof(TKDDI_MEMORY_ALLOC_ERR) },
                { -10170, typeof(TKCSAFE_NO_PM3CSAFE_FILE_ERR) },
                { -10171, typeof(TKCSAFE_UNDEFINED_PM3_CMD_ERR) },
                { -10172, typeof(TKCSAFE_UNDEFINED_DATA_TYPE_ERR) },
                { -10173, typeof(TKCSAFE_UNDEFINED_PM3_RSP_ERR) },
                { -10174, typeof(TKCSAFE_CMD_DATA_EXCEEDS_LIMIT) },
                { -10175, typeof(TKCSAFE_RSP_DATA_EXCEEDS_LIMIT) },
                { -10176, typeof(TKCSAFE_INCORRECT_CMD_PARAMS) },
                { -10177, typeof(TKCSAFE_INCORRECT_RSP_PARAMS) },
                { -10178, typeof(TKCSAFE_LOGCARD_NOT_FOUND) },
                { -10179, typeof(TKCSAFE_LOGCARD_INVALID_ADDRESS) },
                { -10180, typeof(TKCSAFE_LOGCARD_INVALID) },
                { -10181, typeof(TKCSAFE_INI_FILE_CMD_TYPE_INVALID) },
                { -10182, typeof(TKCSAFE_INI_FILE_DATA_TYPE_INVALID) },
                { -10183, typeof(TKCSAFE_LOGCARD_READ_ERROR) },
                { -10184, typeof(TKCSAFE_CSAFE_FRAME_TOO_LONG_ERR) },
                { -10185, typeof(TKCSAFE_INVALID_ADDRESS_IN_RSP_ERR) },
                { -10186, typeof(TKCSAFE_INVALID_ID_IN_RSP_ERR) },
                { -10187, typeof(TKCSAFE_NO_MEMORY_RETURNED) },
                { -10188, typeof(TKCSAFE_AUTHENTICATION_FAILED) },
                { -10189, typeof(TKCSAFE_NULL_POINTER_TO_STRING_ERR) },
                { -10195, typeof(TKLOGCARD_FILE_OPEN_ERROR) },
                { -10196, typeof(TKLOGCARD_EXCEL_LAUNCH_ERROR) },
                { -10197, typeof(TKLOGCARD_FILE_READ_ERROR) },
                { -10200, typeof(TKCMDSET_NO_ECODE_FILE_ERR) },
                { -10201, typeof(TKCMDSET_DEVICE_NOT_FOUND_ERR) },
                { -10202, typeof(TKCMDSET_MUTEX_TIMEOUT_ERR) },
                { -10203, typeof(TKCMDSET_MUTEX_FAILED_ERR) },
                { -10204, typeof(TKCMDSET_INVALID_RESPONSE_ERR) },
                { -10205, typeof(TKCMDSET_UNEXPECTED_RESPONSE_ERR) },
                { -10300, typeof(TKSFILE_END_OF_FILE_ERR) },
                { -10301, typeof(TKSFILE_FILE_READ_ERR) },
                { -10302, typeof(TKSFILE_INVALID_TYPE_ERR) },
                { -10303, typeof(TKSFILE_BAD_CHECKSUM_ERR) },
                { -10304, typeof(TKSFILE_INVALID_LENGTH_ERR) },
                { -10305, typeof(TKSFILE_INVALID_RECORD_ERR) },
                { -10306, typeof(TKSFILE_INVALID_FILE_ERR) },
                { -10307, typeof(TKSFILE_FILE_NOT_FOUND_ERR) },
                { -10308, typeof(TKSFILE_OUTSIDE_BUFFER_ERR) },
                { -10400, typeof(TKCIPHER_NOT_BLOCK_MULT_ERR) },
                { -10500, typeof(CRC_TOOLKIT_ERR) },
                { -10600, typeof(TKREG_CANNOT_CREATE_KEY_ERR) },
                { -10601, typeof(TKREG_NO_OPEN_KEY_ERR) },
                { -10602, typeof(TKREG_STRING_NOT_FOUND_ERR) },
                { -10603, typeof(TKREG_STRING_SAVE_ERR) },
                { -10700, typeof(TKCOM_INVALID_PORT_ERR) },
                { -10701, typeof(TKCOM_INVALID_DEVICE_NAME_ERR) },
                { -10702, typeof(TKCOM_WRITE_FAILED_ERR) },
                { -10703, typeof(TKCOM_WRITE_INCOMPLETE_ERR) },
                { -10704, typeof(TKCOM_WRITE_TIMEOUT_ERR) },
                { -10705, typeof(TKCOM_READ_FAILED_ERR) },
                { -10706, typeof(TKCOM_READ_INCOMPLETE_ERR) },
                { -10707, typeof(TKCOM_READ_TIMEOUT_ERR) },
                { -10708, typeof(TKCOM_DATA_NOT_AVAILABLE_ERR) },
                { -10709, typeof(TKCOM_NO_PORT_INIT_ERR) },
                { -10710, typeof(TKCOM_FLUSH_FAILED_ERR) },
                { -10711, typeof(TKCOM_GET_RXSIZE_ERR) },
                { -10712, typeof(TKCOM_DEVICE_NOT_FOUND_ERR) },
                { -10713, typeof(TKCOM_DTR_ERR) },
                { -10714, typeof(TKCOM_RTS_ERR) },
                { -10715, typeof(TKCOM_DSR_ERR) },
                { -10716, typeof(TKCOM_CTS_ERR) },
                { -10717, typeof(TKCOM_TXBREAK_ERR) },
                { -10718, typeof(TKCOM_CONFIG_ERR) },
                { -10800, typeof(TKNET_WINSOCK_CLOSE_SOCKET_ERR) },
                { -10801, typeof(TKNET_WINSOCK_CREATE_SOCKET_ERR) },
                { -10802, typeof(TKNET_WINSOCK_BIND_SOCKET_ERR) },
                { -10803, typeof(TKNET_WINSOCK_REGISTER_SOCKET_ERR) },
                { -10804, typeof(TKNET_WINSOCK_ENUM_EVENTS_ERR) },
                { -10805, typeof(TKNET_WINSOCK_CREATE_EVENT_ERR) },
                { -10806, typeof(TKNET_WINSOCK_WRITE_ERR) },
                { -10807, typeof(TKNET_READ_TIMEOUT_ERR) },
                { -10808, typeof(TKNET_WINSOCK_READ_ERR) },
                { -10809, typeof(TKNET_WINSOCK_WRONG_EVENT_ERR) },
                { -10810, typeof(TKNET_INCORRECT_PORT_IN_RSP_ERR) },
                { -10811, typeof(TKNET_INCORRECT_IP_IN_RSP_ERR) },
                { -10812, typeof(TKNET_INVALID_PORT_ERR) },
                { -10813, typeof(TKNET_SOCKET_NOT_OPEN_ERR) },
                { -10814, typeof(TKNET_WINSOCK_SETSOCKOPT_ERR) },
                { -10900, typeof(TKRACE_THREAD_CREATE_ERR) },
                { -10901, typeof(TKRACE_MUTEX_CREATE_ERR) },
                { -10902, typeof(TKRACE_MUTEX_TIMEOUT_ERR) },
                { -10903, typeof(TKRACE_MUTEX_FAILED_ERR) },
                { -10904, typeof(TKRACE_WAIT_OBJECT_ERR) },
                { -10905, typeof(TKRACE_EVENT_OBJECT_CREATE_ERR) },
                { -10906, typeof(TKRACE_SET_EVENT_ERR) },
                { -10907, typeof(TKRACE_RESET_EVENT_ERR) },
                { -10908, typeof(TKRACE_NO_PC_TIMER_ERR) },
                { -10909, typeof(TKRACE_INVALID_PM3_TIMEBASE) },
                { -10910, typeof(TKRACE_INVALID_PC_TIMEBASE) },
                { -10911, typeof(TKRACE_PM3_SYNC_ERR) },
                { -10912, typeof(TKRACE_SYNC_ERROR_NO_PM3) },
                { -10913, typeof(TKRACE_SYNC_ALL_DID_NOT_RESPOND) },
                { -10914, typeof(TKRACE_SYNC_ALL_OUT_OF_TOLERANCE) },
                { -10915, typeof(TKRACE_SYNC_COULD_NOT_SYNC) },
                { -10916, typeof(TKRACE_PARTICIPANT_LIST_SET_FAIL) },
                { -20000, typeof(SZUTIL_SEND_ERR) },
                { -20001, typeof(SZUTIL_SYNC_ERR) },
                { -20002, typeof(SZUTIL_BAUD_ERR) },
                { -20003, typeof(SZUTIL_PROGRAMMING_ERR) },
                { -20100, typeof(PM3FLASH_INVALID_CRC_START_ERR) },
                { -20101, typeof(PM3FLASH_INVALID_CRC_END_ERR) },
                { -3000, typeof(TKTSTGEN_NO_UNUSED_FILE_ERR) }
                #endregion
            };

        private readonly ILogger<ExceptionActivator> _logger;

        public ExceptionActivator(ILogger<ExceptionActivator> logger)
        {
            _logger = logger;
        }

        public Exception CreateException(short code)
        {
            if (Exceptions == null)
            {
                Exception e = new InvalidOperationException("An exception was attempted to be created, but the known list of error codes was null.");
                LogError(e);
                throw e;
            }

            if (!Exceptions.ContainsKey(code))
            {
                Exception e = new UnknownException(code);
                LogError(e);
                throw e;
            }

            Type exceptionType = Exceptions[code];

            if (exceptionType == null)
            {
                Exception e = new InvalidOperationException("An exception was attempted to be created, but the targeted type was null.");
                LogError(e);
                throw e;
            }

            object? exception = Activator.CreateInstance(exceptionType);
            
            if (exception == null)
            {
                Exception e = new InvalidOperationException("An exception was attempted to be created, but the class couldn't be activated.");
                LogError(e);
                throw e;
            }

            return (Exception) exception;
        }

        private void LogError(Exception e)
        {
            _logger.LogError(e, "Exception occurred in [{MethodName}]", nameof(CreateException));
        }
    }
}