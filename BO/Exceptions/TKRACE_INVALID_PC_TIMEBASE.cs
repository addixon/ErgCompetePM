using System;

namespace PM.BO.Exceptions
{
	public class TKRACE_INVALID_PC_TIMEBASE : Exception
	{
		private const string _message = "PC timebase invalid (0)..";
		private const short Code = -10910;

		public TKRACE_INVALID_PC_TIMEBASE() : base(_message) { }

		public TKRACE_INVALID_PC_TIMEBASE(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
