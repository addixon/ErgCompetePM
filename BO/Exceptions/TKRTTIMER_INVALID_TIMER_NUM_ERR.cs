using System;

namespace BO.Exceptions
{
	public class TKRTTIMER_INVALID_TIMER_NUM_ERR : Exception
	{
		private const string _message = "The timer number is invalid.";
		private const short Code = -212;

		public TKRTTIMER_INVALID_TIMER_NUM_ERR() : base(_message) { }

		public TKRTTIMER_INVALID_TIMER_NUM_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
