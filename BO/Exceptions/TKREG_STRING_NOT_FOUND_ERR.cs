using System;

namespace BO.Exceptions
{
	public class TKREG_STRING_NOT_FOUND_ERR : Exception
	{
		private const string _message = "The specified string cannot be found in the registry.";
		private const short Code = -10602;

		public TKREG_STRING_NOT_FOUND_ERR() : base(_message) { }

		public TKREG_STRING_NOT_FOUND_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
