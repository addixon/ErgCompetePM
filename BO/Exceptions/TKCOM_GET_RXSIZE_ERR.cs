using System;

namespace BO.Exceptions
{
	public class TKCOM_GET_RXSIZE_ERR : Exception
	{
		private const string _message = "Cannot determine size of receive buffer.";
		private const short Code = -10711;

		public TKCOM_GET_RXSIZE_ERR() : base(_message) { }

		public TKCOM_GET_RXSIZE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
