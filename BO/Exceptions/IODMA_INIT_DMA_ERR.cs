using System;

namespace PM.BO.Exceptions
{
	public class IODMA_INIT_DMA_ERR : Exception
	{
		private const string _message = "DMA initialization error.";
		private const short Code = -822;

		public IODMA_INIT_DMA_ERR() : base(_message) { }

		public IODMA_INIT_DMA_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
