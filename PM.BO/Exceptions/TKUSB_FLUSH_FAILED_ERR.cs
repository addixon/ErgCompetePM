using System;

namespace PM.BO.Exceptions
{
	public class TKUSB_FLUSH_FAILED_ERR : Exception
	{
		private const string _message = "Cannot flush the data from the USB receive buffer.";
		private const short Code = -10110;

		public TKUSB_FLUSH_FAILED_ERR() : base(_message) { }

		public TKUSB_FLUSH_FAILED_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
