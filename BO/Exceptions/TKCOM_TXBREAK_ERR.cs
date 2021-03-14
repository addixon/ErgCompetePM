using System;

namespace PM.BO.Exceptions
{
	public class TKCOM_TXBREAK_ERR : Exception
	{
		private const string _message = "Error controlling a TX BREAK condition.";
		private const short Code = -10717;

		public TKCOM_TXBREAK_ERR() : base(_message) { }

		public TKCOM_TXBREAK_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
