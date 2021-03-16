using System;

namespace PM.BO.Exceptions
{
	public class TKTIME_NO_HIRES_ERR : Exception
	{
		private const string _message = "A high performance counter is not available in this hardware configuration..";
		private const short Code = -10000;

		public TKTIME_NO_HIRES_ERR() : base(_message) { }

		public TKTIME_NO_HIRES_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
