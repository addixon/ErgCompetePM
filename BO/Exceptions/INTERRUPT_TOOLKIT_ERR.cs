using System;

namespace BO.Exceptions
{
	public class INTERRUPT_TOOLKIT_ERR : Exception
	{
		private const string _message = "An interrupt toolkit error occurred.";
		private const short Code = -190;

		public INTERRUPT_TOOLKIT_ERR() : base(_message) { }

		public INTERRUPT_TOOLKIT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
