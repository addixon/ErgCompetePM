using System;

namespace PM.BO.Exceptions
{
	public class IOI2C_GET_RECV_TIMOUT_ERR : Exception
	{
		private const string _message = "I2C receive timeout error.";
		private const short Code = -844;

		public IOI2C_GET_RECV_TIMOUT_ERR() : base(_message) { }

		public IOI2C_GET_RECV_TIMOUT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
