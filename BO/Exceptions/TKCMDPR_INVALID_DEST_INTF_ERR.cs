using System;

namespace BO.Exceptions
{
	public class TKCMDPR_INVALID_DEST_INTF_ERR : Exception
	{
		private const string _message = "An invalid destination interface was specified.";
		private const short Code = -124;

		public TKCMDPR_INVALID_DEST_INTF_ERR() : base(_message) { }

		public TKCMDPR_INVALID_DEST_INTF_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
