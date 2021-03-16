using System;

namespace PM.BO.Exceptions
{
	public class TKUSB_EP_RX_OVERRUN_ERR : Exception
	{
		private const string _message = "The USB receive buffer for the selected endpoint has overrun.";
		private const short Code = -333;

		public TKUSB_EP_RX_OVERRUN_ERR() : base(_message) { }

		public TKUSB_EP_RX_OVERRUN_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
