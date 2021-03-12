using System;

namespace BO.Exceptions
{
	public class TKUSB_SET_FEATURE_ERR : Exception
	{
		private const string _message = "Error sending a feature report to the USB device.";
		private const short Code = -10111;

		public TKUSB_SET_FEATURE_ERR() : base(_message) { }

		public TKUSB_SET_FEATURE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
