using System;

namespace PM.BO.Exceptions
{
	public class IOI2C_WDR_TIMOUT_ERR : Exception
	{
		private const string _message = "I2C Write Data Ready timeout.";
		private const short Code = -846;

		public IOI2C_WDR_TIMOUT_ERR() : base(_message) { }

		public IOI2C_WDR_TIMOUT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
