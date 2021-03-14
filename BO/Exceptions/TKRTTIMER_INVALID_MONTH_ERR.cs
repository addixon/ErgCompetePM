using System;

namespace PM.BO.Exceptions
{
	public class TKRTTIMER_INVALID_MONTH_ERR : Exception
	{
		private const string _message = "The month is invalid.";
		private const short Code = -210;

		public TKRTTIMER_INVALID_MONTH_ERR() : base(_message) { }

		public TKRTTIMER_INVALID_MONTH_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
