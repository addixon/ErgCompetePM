using System;

namespace PM.BO.Exceptions
{
	public class IOMEM_FLASH_ERASE_TIMEOUT_ERR : Exception
	{
		private const string _message = "A timeout occurred while attempting to erase FLASH memory.";
		private const short Code = -870;

		public IOMEM_FLASH_ERASE_TIMEOUT_ERR() : base(_message) { }

		public IOMEM_FLASH_ERASE_TIMEOUT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
