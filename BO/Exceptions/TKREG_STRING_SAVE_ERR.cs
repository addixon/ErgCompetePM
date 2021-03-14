using System;

namespace PM.BO.Exceptions
{
	public class TKREG_STRING_SAVE_ERR : Exception
	{
		private const string _message = "Unable to save the string to the registry.";
		private const short Code = -10603;

		public TKREG_STRING_SAVE_ERR() : base(_message) { }

		public TKREG_STRING_SAVE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
