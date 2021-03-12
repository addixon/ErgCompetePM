using System;

namespace BO.Exceptions
{
	public class TKCMDSET_UNKNOWN_SPECIAL_ERR : Exception
	{
		private const string _message = "The requested special function number is invalid.";
		private const short Code = -310;

		public TKCMDSET_UNKNOWN_SPECIAL_ERR() : base(_message) { }

		public TKCMDSET_UNKNOWN_SPECIAL_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
