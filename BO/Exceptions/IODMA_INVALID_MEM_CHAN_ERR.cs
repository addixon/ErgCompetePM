using System;

namespace BO.Exceptions
{
	public class IODMA_INVALID_MEM_CHAN_ERR : Exception
	{
		private const string _message = "An invalid DMA memory channel was specified.";
		private const short Code = -820;

		public IODMA_INVALID_MEM_CHAN_ERR() : base(_message) { }

		public IODMA_INVALID_MEM_CHAN_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
