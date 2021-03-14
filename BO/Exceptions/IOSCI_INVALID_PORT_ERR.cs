using System;

namespace PM.BO.Exceptions
{
	public class IOSCI_INVALID_PORT_ERR : Exception
	{
		private const string _message = "Invalid SCI port specified.";
		private const short Code = -890;

		public IOSCI_INVALID_PORT_ERR() : base(_message) { }

		public IOSCI_INVALID_PORT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
