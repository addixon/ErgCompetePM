using System;

namespace PM.BO.Exceptions
{
	public class TKSMCD_STOP_ERR : Exception
	{
		private const string _message = "SmartCard stop error.";
		private const short Code = -241;

		public TKSMCD_STOP_ERR() : base(_message) { }

		public TKSMCD_STOP_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
