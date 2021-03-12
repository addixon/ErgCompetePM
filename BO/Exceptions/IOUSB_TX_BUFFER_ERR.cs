using System;

namespace BO.Exceptions
{
	public class IOUSB_TX_BUFFER_ERR : Exception
	{
		private const string _message = "USB transmit buffer error.";
		private const short Code = -937;

		public IOUSB_TX_BUFFER_ERR() : base(_message) { }

		public IOUSB_TX_BUFFER_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
