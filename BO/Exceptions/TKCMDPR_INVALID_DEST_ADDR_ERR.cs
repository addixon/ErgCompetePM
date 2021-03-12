using System;

namespace BO.Exceptions
{
	public class TKCMDPR_INVALID_DEST_ADDR_ERR : Exception
	{
		private const string _message = "An invalid destination address was specified.";
		private const short Code = -123;

		public TKCMDPR_INVALID_DEST_ADDR_ERR() : base(_message) { }

		public TKCMDPR_INVALID_DEST_ADDR_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
