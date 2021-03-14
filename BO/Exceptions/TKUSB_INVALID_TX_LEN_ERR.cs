using System;

namespace PM.BO.Exceptions
{
	public class TKUSB_INVALID_TX_LEN_ERR : Exception
	{
		private const string _message = "The size to transmit exceeds the size of the USB transmit buffer.";
		private const short Code = -341;

		public TKUSB_INVALID_TX_LEN_ERR() : base(_message) { }

		public TKUSB_INVALID_TX_LEN_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
