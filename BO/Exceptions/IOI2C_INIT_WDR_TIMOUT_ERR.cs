using System;

namespace PM.BO.Exceptions
{
	public class IOI2C_INIT_WDR_TIMOUT_ERR : Exception
	{
		private const string _message = "Write Data Ready timeout during initialization.";
		private const short Code = -841;

		public IOI2C_INIT_WDR_TIMOUT_ERR() : base(_message) { }

		public IOI2C_INIT_WDR_TIMOUT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
