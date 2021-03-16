using System;

namespace PM.BO.Exceptions
{
	public class TKUSB_READ_TIMEOUT_ERR : Exception
	{
		private const string _message = "The USB read operation did not complete within the timeout period.";
		private const short Code = -10107;

		public TKUSB_READ_TIMEOUT_ERR() : base(_message) { }

		public TKUSB_READ_TIMEOUT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
