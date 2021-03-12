using System;

namespace BO.Exceptions
{
	public class IODMA_INVALID_IO_RQST_CHAN_ERR : Exception
	{
		private const string _message = "An invalid DMA IO channel was specified.";
		private const short Code = -821;

		public IODMA_INVALID_IO_RQST_CHAN_ERR() : base(_message) { }

		public IODMA_INVALID_IO_RQST_CHAN_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
