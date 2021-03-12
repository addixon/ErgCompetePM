using System;

namespace BO.Exceptions
{
	public class TKEEPROM_INVALID_END_ADDR : Exception
	{
		private const string _message = "Invalid EEPROM end address.";
		private const short Code = -153;

		public TKEEPROM_INVALID_END_ADDR() : base(_message) { }

		public TKEEPROM_INVALID_END_ADDR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
