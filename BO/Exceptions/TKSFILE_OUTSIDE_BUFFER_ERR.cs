using System;

namespace BO.Exceptions
{
	public class TKSFILE_OUTSIDE_BUFFER_ERR : Exception
	{
		private const string _message = "The memory address specified in the S Record file is outside the range of the buffer.";
		private const short Code = -10308;

		public TKSFILE_OUTSIDE_BUFFER_ERR() : base(_message) { }

		public TKSFILE_OUTSIDE_BUFFER_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
