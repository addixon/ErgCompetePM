using System;

namespace PM.BO.Exceptions
{
	public class TKDEBUG_INIT_ERR : Exception
	{
		private const string _message = "Cannot initialize debug functions.";
		private const short Code = -2000;

		public TKDEBUG_INIT_ERR() : base(_message) { }

		public TKDEBUG_INIT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
