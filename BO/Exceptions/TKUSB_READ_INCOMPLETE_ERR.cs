using System;

namespace PM.BO.Exceptions
{
	public class TKUSB_READ_INCOMPLETE_ERR : Exception
	{
		private const string _message = "The USB read operation did not complete.";
		private const short Code = -10106;

		public TKUSB_READ_INCOMPLETE_ERR() : base(_message) { }

		public TKUSB_READ_INCOMPLETE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
