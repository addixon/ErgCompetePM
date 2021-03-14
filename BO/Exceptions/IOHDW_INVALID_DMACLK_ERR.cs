using System;

namespace PM.BO.Exceptions
{
	public class IOHDW_INVALID_DMACLK_ERR : Exception
	{
		private const string _message = "Invalid DMA clock.";
		private const short Code = -831;

		public IOHDW_INVALID_DMACLK_ERR() : base(_message) { }

		public IOHDW_INVALID_DMACLK_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
