using System;

namespace PM.BO.Exceptions
{
	public class IOLCD_DISPINIT_ERR : Exception
	{
		private const string _message = "LCD Display initialization error.";
		private const short Code = -860;

		public IOLCD_DISPINIT_ERR() : base(_message) { }

		public IOLCD_DISPINIT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
