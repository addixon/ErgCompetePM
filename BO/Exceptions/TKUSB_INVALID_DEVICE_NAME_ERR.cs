using System;

namespace BO.Exceptions
{
	public class TKUSB_INVALID_DEVICE_NAME_ERR : Exception
	{
		private const string _message = "The USB device name is invalid.";
		private const short Code = -10101;

		public TKUSB_INVALID_DEVICE_NAME_ERR() : base(_message) { }

		public TKUSB_INVALID_DEVICE_NAME_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
