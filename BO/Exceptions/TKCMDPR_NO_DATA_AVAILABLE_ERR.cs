using System;

namespace BO.Exceptions
{
	public class TKCMDPR_NO_DATA_AVAILABLE_ERR : Exception
	{
		private const string _message = "No data is available.";
		private const short Code = -127;

		public TKCMDPR_NO_DATA_AVAILABLE_ERR() : base(_message) { }

		public TKCMDPR_NO_DATA_AVAILABLE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
