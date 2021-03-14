using System;

namespace PM.BO.Exceptions
{
	public class TKUSB_WRITE_INCOMPLETE_ERR : Exception
	{
		private const string _message = "The write operation did not complete.";
		private const short Code = -10103;

		public TKUSB_WRITE_INCOMPLETE_ERR() : base(_message) { }

		public TKUSB_WRITE_INCOMPLETE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
