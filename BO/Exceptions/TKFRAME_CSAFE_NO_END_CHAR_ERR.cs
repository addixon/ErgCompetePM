using System;

namespace BO.Exceptions
{
	public class TKFRAME_CSAFE_NO_END_CHAR_ERR : Exception
	{
		private const string _message = "CSAFE frame missing end character.";
		private const short Code = -166;

		public TKFRAME_CSAFE_NO_END_CHAR_ERR() : base(_message) { }

		public TKFRAME_CSAFE_NO_END_CHAR_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
