using System;

namespace PM.BO.Exceptions
{
	public class TKDISP_STRING_TOO_LONG_ERR : Exception
	{
		private const string _message = "The string is too long.";
		private const short Code = -142;

		public TKDISP_STRING_TOO_LONG_ERR() : base(_message) { }

		public TKDISP_STRING_TOO_LONG_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
