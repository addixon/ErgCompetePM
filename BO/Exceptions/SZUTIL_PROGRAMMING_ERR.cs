using System;

namespace BO.Exceptions
{
	public class SZUTIL_PROGRAMMING_ERR : Exception
	{
		private const string _message = "Flash programming failed.";
		private const short Code = -20003;

		public SZUTIL_PROGRAMMING_ERR() : base(_message) { }

		public SZUTIL_PROGRAMMING_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
