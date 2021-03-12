using System;

namespace BO.Exceptions
{
	public class IOTIMER_INVALID_TIMERID_ERR : Exception
	{
		private const string _message = "Invalid timer identifier specified.";
		private const short Code = -910;

		public IOTIMER_INVALID_TIMERID_ERR() : base(_message) { }

		public IOTIMER_INVALID_TIMERID_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
