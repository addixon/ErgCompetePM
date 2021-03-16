using System;

namespace PM.BO.Exceptions
{
	public class IOUSB_WAKEUP_DISABLE_ERR : Exception
	{
		private const string _message = "USB wakeup disable error.";
		private const short Code = -940;

		public IOUSB_WAKEUP_DISABLE_ERR() : base(_message) { }

		public IOUSB_WAKEUP_DISABLE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
