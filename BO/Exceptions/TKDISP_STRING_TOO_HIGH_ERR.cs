using System;

namespace PM.BO.Exceptions
{
	public class TKDISP_STRING_TOO_HIGH_ERR : Exception
	{
		private const string _message = "The string is too high.";
		private const short Code = -143;

		public TKDISP_STRING_TOO_HIGH_ERR() : base(_message) { }

		public TKDISP_STRING_TOO_HIGH_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
