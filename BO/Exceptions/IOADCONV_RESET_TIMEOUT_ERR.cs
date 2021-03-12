using System;

namespace BO.Exceptions
{
	public class IOADCONV_RESET_TIMEOUT_ERR : Exception
	{
		private const string _message = "A reset timeout occurred.";
		private const short Code = -811;

		public IOADCONV_RESET_TIMEOUT_ERR() : base(_message) { }

		public IOADCONV_RESET_TIMEOUT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
