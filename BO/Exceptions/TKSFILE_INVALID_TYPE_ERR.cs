using System;

namespace BO.Exceptions
{
	public class TKSFILE_INVALID_TYPE_ERR : Exception
	{
		private const string _message = "The S Record file is not a valid type.";
		private const short Code = -10302;

		public TKSFILE_INVALID_TYPE_ERR() : base(_message) { }

		public TKSFILE_INVALID_TYPE_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
