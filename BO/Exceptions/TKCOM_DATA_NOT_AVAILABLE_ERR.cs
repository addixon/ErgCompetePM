using System;

namespace PM.BO.Exceptions
{
	public class TKCOM_DATA_NOT_AVAILABLE_ERR : Exception
	{
		private const string _message = "No data available.";
		private const short Code = -10708;

		public TKCOM_DATA_NOT_AVAILABLE_ERR() : base(_message) { }

		public TKCOM_DATA_NOT_AVAILABLE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
