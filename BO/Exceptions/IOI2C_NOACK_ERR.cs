using System;

namespace BO.Exceptions
{
	public class IOI2C_NOACK_ERR : Exception
	{
		private const string _message = "No I2C acknowledge.";
		private const short Code = -840;

		public IOI2C_NOACK_ERR() : base(_message) { }

		public IOI2C_NOACK_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
