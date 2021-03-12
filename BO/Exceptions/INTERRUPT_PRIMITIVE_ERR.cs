using System;

namespace BO.Exceptions
{
	public class INTERRUPT_PRIMITIVE_ERR : Exception
	{
		private const string _message = "An interrupt primitive error occurred.";
		private const short Code = -850;

		public INTERRUPT_PRIMITIVE_ERR() : base(_message) { }

		public INTERRUPT_PRIMITIVE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
