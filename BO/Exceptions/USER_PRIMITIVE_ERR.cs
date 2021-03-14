using System;

namespace PM.BO.Exceptions
{
	public class USER_PRIMITIVE_ERR : Exception
	{
		private const string _message = "A User Primitive Error Occurred.";
		private const short Code = -920;

		public USER_PRIMITIVE_ERR() : base(_message) { }

		public USER_PRIMITIVE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
