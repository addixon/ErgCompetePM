using System;

namespace BO.Exceptions
{
	public class TKCOM_WRITE_INCOMPLETE_ERR : Exception
	{
		private const string _message = "Write operation incomplete.";
		private const short Code = -10703;

		public TKCOM_WRITE_INCOMPLETE_ERR() : base(_message) { }

		public TKCOM_WRITE_INCOMPLETE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
