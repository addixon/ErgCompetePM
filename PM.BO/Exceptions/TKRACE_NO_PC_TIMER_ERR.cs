using System;

namespace PM.BO.Exceptions
{
	public class TKRACE_NO_PC_TIMER_ERR : Exception
	{
		private const string _message = "High resolution timer not supported..";
		private const short Code = -10908;

		public TKRACE_NO_PC_TIMER_ERR() : base(_message) { }

		public TKRACE_NO_PC_TIMER_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
