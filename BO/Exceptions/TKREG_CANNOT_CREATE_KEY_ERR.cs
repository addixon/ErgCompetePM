using System;

namespace BO.Exceptions
{
	public class TKREG_CANNOT_CREATE_KEY_ERR : Exception
	{
		private const string _message = "The specified registry key cannot be created.";
		private const short Code = -10600;

		public TKREG_CANNOT_CREATE_KEY_ERR() : base(_message) { }

		public TKREG_CANNOT_CREATE_KEY_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
