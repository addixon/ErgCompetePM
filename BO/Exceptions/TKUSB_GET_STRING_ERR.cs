using System;

namespace BO.Exceptions
{
	public class TKUSB_GET_STRING_ERR : Exception
	{
		private const string _message = "Unable to read USB HID string information.";
		private const short Code = -10112;

		public TKUSB_GET_STRING_ERR() : base(_message) { }

		public TKUSB_GET_STRING_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
