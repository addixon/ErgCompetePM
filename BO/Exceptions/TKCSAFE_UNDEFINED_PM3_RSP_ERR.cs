using System;

namespace PM.BO.Exceptions
{
	public class TKCSAFE_UNDEFINED_PM3_RSP_ERR : Exception
	{
		private const string _message = "PM3 response not defined in the INI file..";
		private const short Code = -10173;

		public TKCSAFE_UNDEFINED_PM3_RSP_ERR() : base(_message) { }

		public TKCSAFE_UNDEFINED_PM3_RSP_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
