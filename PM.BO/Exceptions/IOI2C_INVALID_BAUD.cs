using System;

namespace PM.BO.Exceptions
{
	public class IOI2C_INVALID_BAUD : Exception
	{
		private const string _message = "Invalid I2C baud.";
		private const short Code = -847;

		public IOI2C_INVALID_BAUD() : base(_message) { }

		public IOI2C_INVALID_BAUD(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
