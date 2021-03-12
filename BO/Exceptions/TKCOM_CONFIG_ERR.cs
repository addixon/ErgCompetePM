using System;

namespace BO.Exceptions
{
	public class TKCOM_CONFIG_ERR : Exception
	{
		private const string _message = "Cannot configure communication port settings, make sure selected COM port is available.  Close any DOS windows that may be using this port..";
		private const short Code = -10718;

		public TKCOM_CONFIG_ERR() : base(_message) { }

		public TKCOM_CONFIG_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
