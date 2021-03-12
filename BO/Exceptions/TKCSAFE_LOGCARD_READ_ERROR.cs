using System;

namespace BO.Exceptions
{
	public class TKCSAFE_LOGCARD_READ_ERROR : Exception
	{
		private const string _message = "LogCard read error..";
		private const short Code = -10183;

		public TKCSAFE_LOGCARD_READ_ERROR() : base(_message) { }

		public TKCSAFE_LOGCARD_READ_ERROR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
