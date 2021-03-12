using System;

namespace BO.Exceptions
{
	public class TKCSAFE_INI_FILE_DATA_TYPE_INVALID : Exception
	{
		private const string _message = "Invalid data type in initialization file..";
		private const short Code = -10182;

		public TKCSAFE_INI_FILE_DATA_TYPE_INVALID() : base(_message) { }

		public TKCSAFE_INI_FILE_DATA_TYPE_INVALID(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
