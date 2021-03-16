using System;

namespace PM.BO.Exceptions
{
	public class TKCMDSET_INVALID_RESPONSE_ERR : Exception
	{
		private const string _message = "An invalid response was returned by the device.";
		private const short Code = -10204;

		public TKCMDSET_INVALID_RESPONSE_ERR() : base(_message) { }

		public TKCMDSET_INVALID_RESPONSE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
