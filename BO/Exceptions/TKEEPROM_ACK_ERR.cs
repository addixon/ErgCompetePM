using System;

namespace BO.Exceptions
{
	public class TKEEPROM_ACK_ERR : Exception
	{
		private const string _message = "I2C Acknowledge error communicating with EEPROM.";
		private const short Code = -151;

		public TKEEPROM_ACK_ERR() : base(_message) { }

		public TKEEPROM_ACK_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
