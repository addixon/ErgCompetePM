using System;

namespace BO.Exceptions
{
	public class TKCMDPR_INVALID_CMD_ADDR_ERR : Exception
	{
		private const string _message = "An invalid address was used.";
		private const short Code = -122;

		public TKCMDPR_INVALID_CMD_ADDR_ERR() : base(_message) { }

		public TKCMDPR_INVALID_CMD_ADDR_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
