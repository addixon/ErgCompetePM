using System;

namespace PM.BO.Exceptions
{
	public class TKDDI_PROTOCOL_INIT_ERR : Exception
	{
		private const string _message = "Protocol init failed. Null pointer passed..";
		private const short Code = -10153;

		public TKDDI_PROTOCOL_INIT_ERR() : base(_message) { }

		public TKDDI_PROTOCOL_INIT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
