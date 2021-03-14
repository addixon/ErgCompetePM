using System;

namespace PM.BO.Exceptions
{
	public class TKDDI_PROTOCOL_NOT_DEFINED_ERR : Exception
	{
		private const string _message = "Protocol not initialized..";
		private const short Code = -10154;

		public TKDDI_PROTOCOL_NOT_DEFINED_ERR() : base(_message) { }

		public TKDDI_PROTOCOL_NOT_DEFINED_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
