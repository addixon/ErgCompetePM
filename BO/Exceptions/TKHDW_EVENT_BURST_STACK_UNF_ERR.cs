using System;

namespace BO.Exceptions
{
	public class TKHDW_EVENT_BURST_STACK_UNF_ERR : Exception
	{
		private const string _message = "Burst stack underflow error.";
		private const short Code = -171;

		public TKHDW_EVENT_BURST_STACK_UNF_ERR() : base(_message) { }

		public TKHDW_EVENT_BURST_STACK_UNF_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
