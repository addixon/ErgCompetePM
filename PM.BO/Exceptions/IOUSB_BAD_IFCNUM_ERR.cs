using System;

namespace PM.BO.Exceptions
{
	public class IOUSB_BAD_IFCNUM_ERR : Exception
	{
		private const string _message = "An invalid interface number was specified.";
		private const short Code = -943;

		public IOUSB_BAD_IFCNUM_ERR() : base(_message) { }

		public IOUSB_BAD_IFCNUM_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
