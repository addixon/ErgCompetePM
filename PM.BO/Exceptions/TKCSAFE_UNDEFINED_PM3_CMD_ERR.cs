using System;

namespace PM.BO.Exceptions
{
	public class TKCSAFE_UNDEFINED_PM3_CMD_ERR : Exception
	{
		private const string _message = "PM3 command not defined in the INI file or too many command parameters..";
		private const short Code = -10171;

		public TKCSAFE_UNDEFINED_PM3_CMD_ERR() : base(_message) { }

		public TKCSAFE_UNDEFINED_PM3_CMD_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
