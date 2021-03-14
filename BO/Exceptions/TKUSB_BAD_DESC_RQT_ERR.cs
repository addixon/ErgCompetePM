using System;

namespace PM.BO.Exceptions
{
	public class TKUSB_BAD_DESC_RQT_ERR : Exception
	{
		private const string _message = "Bad USB descriptor request.";
		private const short Code = -330;

		public TKUSB_BAD_DESC_RQT_ERR() : base(_message) { }

		public TKUSB_BAD_DESC_RQT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
