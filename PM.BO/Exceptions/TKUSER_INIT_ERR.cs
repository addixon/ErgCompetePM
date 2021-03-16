using System;

namespace PM.BO.Exceptions
{
	public class TKUSER_INIT_ERR : Exception
	{
		private const string _message = "User function initialization error.";
		private const short Code = -280;

		public TKUSER_INIT_ERR() : base(_message) { }

		public TKUSER_INIT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
