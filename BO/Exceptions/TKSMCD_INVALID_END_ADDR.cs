using System;

namespace BO.Exceptions
{
	public class TKSMCD_INVALID_END_ADDR : Exception
	{
		private const string _message = "Invalid SmartCard end address.";
		private const short Code = -242;

		public TKSMCD_INVALID_END_ADDR() : base(_message) { }

		public TKSMCD_INVALID_END_ADDR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
