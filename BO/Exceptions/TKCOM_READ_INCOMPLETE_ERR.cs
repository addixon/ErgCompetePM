using System;

namespace BO.Exceptions
{
	public class TKCOM_READ_INCOMPLETE_ERR : Exception
	{
		private const string _message = "Read operation incomplete.";
		private const short Code = -10706;

		public TKCOM_READ_INCOMPLETE_ERR() : base(_message) { }

		public TKCOM_READ_INCOMPLETE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
