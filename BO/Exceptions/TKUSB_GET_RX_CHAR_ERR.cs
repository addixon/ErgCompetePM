using System;

namespace PM.BO.Exceptions
{
	public class TKUSB_GET_RX_CHAR_ERR : Exception
	{
		private const string _message = "Cannot get a character from the USB buffer.";
		private const short Code = -335;

		public TKUSB_GET_RX_CHAR_ERR() : base(_message) { }

		public TKUSB_GET_RX_CHAR_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
