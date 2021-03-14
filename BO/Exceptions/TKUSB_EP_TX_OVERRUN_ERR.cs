using System;

namespace PM.BO.Exceptions
{
	public class TKUSB_EP_TX_OVERRUN_ERR : Exception
	{
		private const string _message = "The USB transmit buffer for the selected endpoint has overrun.";
		private const short Code = -340;

		public TKUSB_EP_TX_OVERRUN_ERR() : base(_message) { }

		public TKUSB_EP_TX_OVERRUN_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
