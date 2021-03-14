using System;

namespace PM.BO.Exceptions
{
	public class TKSMCD_WRITE_READ_ERR : Exception
	{
		private const string _message = "SmartCard write / read error.";
		private const short Code = -244;

		public TKSMCD_WRITE_READ_ERR() : base(_message) { }

		public TKSMCD_WRITE_READ_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
