using System;

namespace BO.Exceptions
{
	public class TKCSAFE_LOGCARD_NOT_FOUND : Exception
	{
		private const string _message = "LogCard not found. Check to see that it is inserted properly..";
		private const short Code = -10178;

		public TKCSAFE_LOGCARD_NOT_FOUND() : base(_message) { }

		public TKCSAFE_LOGCARD_NOT_FOUND(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
