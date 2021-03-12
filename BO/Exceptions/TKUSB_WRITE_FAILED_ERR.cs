using System;

namespace BO.Exceptions
{
	public class TKUSB_WRITE_FAILED_ERR : Exception
	{
		private const string _message = "Could not send data to the USB device.";
		private const short Code = -10102;

		public TKUSB_WRITE_FAILED_ERR() : base(_message) { }

		public TKUSB_WRITE_FAILED_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
