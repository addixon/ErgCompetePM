using System;

namespace PM.BO.Exceptions
{
	public class TKUSB_READ_FAILED_ERR : Exception
	{
		private const string _message = "Could not read data from the USB device.";
		private const short Code = -10105;

		public TKUSB_READ_FAILED_ERR() : base(_message) { }

		public TKUSB_READ_FAILED_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
