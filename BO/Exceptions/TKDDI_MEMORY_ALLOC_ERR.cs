using System;

namespace BO.Exceptions
{
	public class TKDDI_MEMORY_ALLOC_ERR : Exception
	{
		private const string _message = "TKDDI memory allocation error..";
		private const short Code = -10155;

		public TKDDI_MEMORY_ALLOC_ERR() : base(_message) { }

		public TKDDI_MEMORY_ALLOC_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
