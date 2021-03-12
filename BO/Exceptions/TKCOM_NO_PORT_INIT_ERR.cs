using System;

namespace BO.Exceptions
{
	public class TKCOM_NO_PORT_INIT_ERR : Exception
	{
		private const string _message = "The specified port identifier is not associated with an initialized communication port.";
		private const short Code = -10709;

		public TKCOM_NO_PORT_INIT_ERR() : base(_message) { }

		public TKCOM_NO_PORT_INIT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
