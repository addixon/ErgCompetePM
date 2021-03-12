using System;

namespace BO.Exceptions
{
	public class IOI2C_STOP_TIMEOUT_ERR : Exception
	{
		private const string _message = "I2C stop timeout error.";
		private const short Code = -845;

		public IOI2C_STOP_TIMEOUT_ERR() : base(_message) { }

		public IOI2C_STOP_TIMEOUT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
