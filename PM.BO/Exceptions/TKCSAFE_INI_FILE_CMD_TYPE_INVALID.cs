using System;

namespace PM.BO.Exceptions
{
	public class TKCSAFE_INI_FILE_CMD_TYPE_INVALID : Exception
	{
		private const string _message = "Invalid command type in initialization file..";
		private const short Code = -10181;

		public TKCSAFE_INI_FILE_CMD_TYPE_INVALID() : base(_message) { }

		public TKCSAFE_INI_FILE_CMD_TYPE_INVALID(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
