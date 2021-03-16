using System;

namespace PM.BO.Exceptions
{
	public class TKCSAFE_UNDEFINED_DATA_TYPE_ERR : Exception
	{
		private const string _message = "Undefined data type in PM3CSAFE.INI file..";
		private const short Code = -10172;

		public TKCSAFE_UNDEFINED_DATA_TYPE_ERR() : base(_message) { }

		public TKCSAFE_UNDEFINED_DATA_TYPE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
