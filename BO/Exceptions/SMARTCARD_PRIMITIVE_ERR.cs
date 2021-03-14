using System;

namespace PM.BO.Exceptions
{
	public class SMARTCARD_PRIMITIVE_ERR : Exception
	{
		private const string _message = "A smartcard primitive error occurred.";
		private const short Code = -900;

		public SMARTCARD_PRIMITIVE_ERR() : base(_message) { }

		public SMARTCARD_PRIMITIVE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
