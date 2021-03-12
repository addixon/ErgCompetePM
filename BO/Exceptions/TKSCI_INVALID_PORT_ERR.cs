using System;

namespace BO.Exceptions
{
	public class TKSCI_INVALID_PORT_ERR : Exception
	{
		private const string _message = "Invalid SCI port.";
		private const short Code = -220;

		public TKSCI_INVALID_PORT_ERR() : base(_message) { }

		public TKSCI_INVALID_PORT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
