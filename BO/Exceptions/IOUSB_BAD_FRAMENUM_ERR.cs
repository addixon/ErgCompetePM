using System;

namespace PM.BO.Exceptions
{
	public class IOUSB_BAD_FRAMENUM_ERR : Exception
	{
		private const string _message = "A bad USB frame was detected.";
		private const short Code = -941;

		public IOUSB_BAD_FRAMENUM_ERR() : base(_message) { }

		public IOUSB_BAD_FRAMENUM_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
