using System;

namespace PM.BO.Exceptions
{
	public class TKSCI_RX_TIMEOUT_ERR : Exception
	{
		private const string _message = "SCI receive timeout error.";
		private const short Code = -222;

		public TKSCI_RX_TIMEOUT_ERR() : base(_message) { }

		public TKSCI_RX_TIMEOUT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
