using System;

namespace BO.Exceptions
{
	public class TKLOGCARD_FILE_READ_ERROR : Exception
	{
		private const string _message = "Error opening binary file or cancel pressed..";
		private const short Code = -10197;

		public TKLOGCARD_FILE_READ_ERROR() : base(_message) { }

		public TKLOGCARD_FILE_READ_ERROR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
