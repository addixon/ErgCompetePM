using System;

namespace PM.BO.Exceptions
{
	public class TKCMDPR_INVALID_INTF_ERR : Exception
	{
		private const string _message = "An invalid interface was specified.";
		private const short Code = -125;

		public TKCMDPR_INVALID_INTF_ERR() : base(_message) { }

		public TKCMDPR_INVALID_INTF_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
