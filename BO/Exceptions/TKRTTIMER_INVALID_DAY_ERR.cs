using System;

namespace BO.Exceptions
{
	public class TKRTTIMER_INVALID_DAY_ERR : Exception
	{
		private const string _message = "The day is invalid.";
		private const short Code = -211;

		public TKRTTIMER_INVALID_DAY_ERR() : base(_message) { }

		public TKRTTIMER_INVALID_DAY_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
