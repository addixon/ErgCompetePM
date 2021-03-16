using System;

namespace PM.BO.Exceptions
{
	public class TKCSAFE_NO_PM3CSAFE_FILE_ERR : Exception
	{
		private const string _message = "The PM3CSAFE.INI file is not available, PM3 commands will not be available..";
		private const short Code = -10170;

		public TKCSAFE_NO_PM3CSAFE_FILE_ERR() : base(_message) { }

		public TKCSAFE_NO_PM3CSAFE_FILE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
