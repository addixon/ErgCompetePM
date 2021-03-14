using System;

namespace PM.BO.Exceptions
{
	public class TKCRC_ERR : Exception
	{
		private const string _message = "Error computing CRC.";
		private const short Code = -300;

		public TKCRC_ERR() : base(_message) { }

		public TKCRC_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
