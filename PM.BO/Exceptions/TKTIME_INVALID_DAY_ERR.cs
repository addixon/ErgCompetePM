using System;

namespace PM.BO.Exceptions
{
	public class TKTIME_INVALID_DAY_ERR : Exception
	{
		private const string _message = "The day is invalid.";
		private const short Code = -261;

		public TKTIME_INVALID_DAY_ERR() : base(_message) { }

		public TKTIME_INVALID_DAY_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
