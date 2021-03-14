using System;

namespace PM.BO.Exceptions
{
	public class TKREG_NO_OPEN_KEY_ERR : Exception
	{
		private const string _message = "No registry key is currently open.";
		private const short Code = -10601;

		public TKREG_NO_OPEN_KEY_ERR() : base(_message) { }

		public TKREG_NO_OPEN_KEY_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
