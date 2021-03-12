using System;

namespace BO.Exceptions
{
	public class TKUSB_INVALID_STRING_ID_ERR : Exception
	{
		private const string _message = "The identifier for a USB string is invalid.";
		private const short Code = -339;

		public TKUSB_INVALID_STRING_ID_ERR() : base(_message) { }

		public TKUSB_INVALID_STRING_ID_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
