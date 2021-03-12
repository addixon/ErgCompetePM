using System;

namespace BO.Exceptions
{
	public class TKEEPROM_WRITE_VERIFY_ERR : Exception
	{
		private const string _message = "The EEPROM data written could not be verified.";
		private const short Code = -156;

		public TKEEPROM_WRITE_VERIFY_ERR() : base(_message) { }

		public TKEEPROM_WRITE_VERIFY_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
