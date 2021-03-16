using System;

namespace PM.BO.Exceptions
{
	public class TKCOM_DTR_ERR : Exception
	{
		private const string _message = "Error controlling the DTR signal.";
		private const short Code = -10713;

		public TKCOM_DTR_ERR() : base(_message) { }

		public TKCOM_DTR_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
