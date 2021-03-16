using System;

namespace PM.BO.Exceptions
{
	public class TKUSER_PEN_INIT_ERR : Exception
	{
		private const string _message = "Pen input initialization error.";
		private const short Code = -281;

		public TKUSER_PEN_INIT_ERR() : base(_message) { }

		public TKUSER_PEN_INIT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
