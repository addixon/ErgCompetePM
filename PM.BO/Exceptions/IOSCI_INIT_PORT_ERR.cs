using System;

namespace PM.BO.Exceptions
{
	public class IOSCI_INIT_PORT_ERR : Exception
	{
		private const string _message = "SCI port initialization error.";
		private const short Code = -893;

		public IOSCI_INIT_PORT_ERR() : base(_message) { }

		public IOSCI_INIT_PORT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
