using System;

namespace BO.Exceptions
{
	public class TKUSB_NO_PORT_INIT_ERR : Exception
	{
		private const string _message = "The USB port has not been initialized.";
		private const short Code = -10109;

		public TKUSB_NO_PORT_INIT_ERR() : base(_message) { }

		public TKUSB_NO_PORT_INIT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
