using System;

namespace BO.Exceptions
{
	public class PM3FLASH_INVALID_CRC_START_ERR : Exception
	{
		private const string _message = "The start address for a CRC block is invalid.";
		private const short Code = -20100;

		public PM3FLASH_INVALID_CRC_START_ERR() : base(_message) { }

		public PM3FLASH_INVALID_CRC_START_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
