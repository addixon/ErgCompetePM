using System;

namespace BO.Exceptions
{
	public class TKHDW_EVENT_BURST_STACK_OVF_ERR : Exception
	{
		private const string _message = "Burst stack overflow error.";
		private const short Code = -170;

		public TKHDW_EVENT_BURST_STACK_OVF_ERR() : base(_message) { }

		public TKHDW_EVENT_BURST_STACK_OVF_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
