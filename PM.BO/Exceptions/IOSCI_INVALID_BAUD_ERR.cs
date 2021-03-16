using System;

namespace PM.BO.Exceptions
{
	public class IOSCI_INVALID_BAUD_ERR : Exception
	{
		private const string _message = "Invalid baud rate for SCI port.";
		private const short Code = -891;

		public IOSCI_INVALID_BAUD_ERR() : base(_message) { }

		public IOSCI_INVALID_BAUD_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
