using System;

namespace BO.Exceptions
{
	public class TKTIME_INVALID_MONTH_ERR : Exception
	{
		private const string _message = "The month is invalid.";
		private const short Code = -260;

		public TKTIME_INVALID_MONTH_ERR() : base(_message) { }

		public TKTIME_INVALID_MONTH_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
