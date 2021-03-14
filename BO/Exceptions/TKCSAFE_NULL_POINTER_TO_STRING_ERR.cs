using System;

namespace PM.BO.Exceptions
{
	public class TKCSAFE_NULL_POINTER_TO_STRING_ERR : Exception
	{
		private const string _message = "CSAFE null pointer to string in command..";
		private const short Code = -10189;

		public TKCSAFE_NULL_POINTER_TO_STRING_ERR() : base(_message) { }

		public TKCSAFE_NULL_POINTER_TO_STRING_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
