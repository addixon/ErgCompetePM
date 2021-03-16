using System;

namespace PM.BO.Exceptions
{
	public class SZUTIL_BAUD_ERR : Exception
	{
		private const string _message = "Cannot change baud rate.";
		private const short Code = -20002;

		public SZUTIL_BAUD_ERR() : base(_message) { }

		public SZUTIL_BAUD_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
