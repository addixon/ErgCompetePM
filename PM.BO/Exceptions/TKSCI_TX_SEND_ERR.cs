using System;

namespace PM.BO.Exceptions
{
	public class TKSCI_TX_SEND_ERR : Exception
	{
		private const string _message = "Error sending data via SCI.";
		private const short Code = -221;

		public TKSCI_TX_SEND_ERR() : base(_message) { }

		public TKSCI_TX_SEND_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
