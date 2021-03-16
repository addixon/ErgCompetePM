using System;

namespace PM.BO.Exceptions
{
	public class IOMEM_FLASH_WRITE_TIMEOUT_ERR : Exception
	{
		private const string _message = "A timeout occurred while attempting to write FLASH memory.";
		private const short Code = -871;

		public IOMEM_FLASH_WRITE_TIMEOUT_ERR() : base(_message) { }

		public IOMEM_FLASH_WRITE_TIMEOUT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
