using System;

namespace BO.Exceptions
{
	public class IOHDW_INVALID_SYSCLK_ERR : Exception
	{
		private const string _message = "Invalid System clock.";
		private const short Code = -832;

		public IOHDW_INVALID_SYSCLK_ERR() : base(_message) { }

		public IOHDW_INVALID_SYSCLK_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
