using System;

namespace BO.Exceptions
{
	public class TKCMDSET_UNEXPECTED_RESPONSE_ERR : Exception
	{
		private const string _message = "The expected response was not returned by the device.";
		private const short Code = -10205;

		public TKCMDSET_UNEXPECTED_RESPONSE_ERR() : base(_message) { }

		public TKCMDSET_UNEXPECTED_RESPONSE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
