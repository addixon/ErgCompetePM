using System;

namespace PM.BO.Exceptions
{
	public class TKEXP_RS232_INVALID_ERR : Exception
	{
		private const string _message = "Invalid RS232 on expansion card.";
		private const short Code = -1000;

		public TKEXP_RS232_INVALID_ERR() : base(_message) { }

		public TKEXP_RS232_INVALID_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
