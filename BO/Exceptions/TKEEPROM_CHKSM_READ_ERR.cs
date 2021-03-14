using System;

namespace PM.BO.Exceptions
{
	public class TKEEPROM_CHKSM_READ_ERR : Exception
	{
		private const string _message = "Error reading the EEPROM checksum information.";
		private const short Code = -157;

		public TKEEPROM_CHKSM_READ_ERR() : base(_message) { }

		public TKEEPROM_CHKSM_READ_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
