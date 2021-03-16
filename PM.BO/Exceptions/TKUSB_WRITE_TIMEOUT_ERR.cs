using System;

namespace PM.BO.Exceptions
{
	public class TKUSB_WRITE_TIMEOUT_ERR : Exception
	{
		private const string _message = "The write operation did not complete within the timeout period.";
		private const short Code = -10104;

		public TKUSB_WRITE_TIMEOUT_ERR() : base(_message) { }

		public TKUSB_WRITE_TIMEOUT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
