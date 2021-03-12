using System;

namespace BO.Exceptions
{
	public class IOADCONV_BG_TIMEOUT_ERR : Exception
	{
		private const string _message = "A background timeout occurred.";
		private const short Code = -810;

		public IOADCONV_BG_TIMEOUT_ERR() : base(_message) { }

		public IOADCONV_BG_TIMEOUT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
