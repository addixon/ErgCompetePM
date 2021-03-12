using System;

namespace BO.Exceptions
{
	public class TKDISP_INVALID_CHAR_ERR : Exception
	{
		private const string _message = "Invalid character.";
		private const short Code = -140;

		public TKDISP_INVALID_CHAR_ERR() : base(_message) { }

		public TKDISP_INVALID_CHAR_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
