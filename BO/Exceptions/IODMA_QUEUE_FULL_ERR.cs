using System;

namespace PM.BO.Exceptions
{
	public class IODMA_QUEUE_FULL_ERR : Exception
	{
		private const string _message = "The DMA processing queue is full.";
		private const short Code = -823;

		public IODMA_QUEUE_FULL_ERR() : base(_message) { }

		public IODMA_QUEUE_FULL_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
