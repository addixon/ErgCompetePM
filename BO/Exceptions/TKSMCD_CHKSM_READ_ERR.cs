using System;

namespace BO.Exceptions
{
	public class TKSMCD_CHKSM_READ_ERR : Exception
	{
		private const string _message = "Error reading the SmartCard checksum.";
		private const short Code = -246;

		public TKSMCD_CHKSM_READ_ERR() : base(_message) { }

		public TKSMCD_CHKSM_READ_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
