using System;

namespace BO.Exceptions
{
	public class TKUSB_INVALID_PORT_ERR : Exception
	{
		private const string _message = "An invalid port number was specified.";
		private const short Code = -10100;

		public TKUSB_INVALID_PORT_ERR() : base(_message) { }

		public TKUSB_INVALID_PORT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
