using System;

namespace BO.Exceptions
{
	public class TKRACE_INVALID_PM3_TIMEBASE : Exception
	{
		private const string _message = "PM3 timebase invalid (0)..";
		private const short Code = -10909;

		public TKRACE_INVALID_PM3_TIMEBASE() : base(_message) { }

		public TKRACE_INVALID_PM3_TIMEBASE(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
