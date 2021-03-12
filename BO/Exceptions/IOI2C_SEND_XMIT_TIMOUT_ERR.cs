using System;

namespace BO.Exceptions
{
	public class IOI2C_SEND_XMIT_TIMOUT_ERR : Exception
	{
		private const string _message = "I2C transmit timeout during send.";
		private const short Code = -843;

		public IOI2C_SEND_XMIT_TIMOUT_ERR() : base(_message) { }

		public IOI2C_SEND_XMIT_TIMOUT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
