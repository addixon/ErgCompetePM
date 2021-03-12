using System;

namespace BO.Exceptions
{
	public class TKUSB_RX_TIMEOUT_ERR : Exception
	{
		private const string _message = "USB receive timeout error.";
		private const short Code = -332;

		public TKUSB_RX_TIMEOUT_ERR() : base(_message) { }

		public TKUSB_RX_TIMEOUT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
