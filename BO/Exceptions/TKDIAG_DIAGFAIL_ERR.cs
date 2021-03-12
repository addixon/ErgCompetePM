using System;

namespace BO.Exceptions
{
	public class TKDIAG_DIAGFAIL_ERR : Exception
	{
		private const string _message = "An error was detected during diagnostics.";
		private const short Code = -500;

		public TKDIAG_DIAGFAIL_ERR() : base(_message) { }

		public TKDIAG_DIAGFAIL_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
