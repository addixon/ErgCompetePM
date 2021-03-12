using System;

namespace BO.Exceptions
{
	public class TKFRAME_CSAFE_FRAME_STUFF_ERR : Exception
	{
		private const string _message = "Error stuffing CSAFE frame.";
		private const short Code = -160;

		public TKFRAME_CSAFE_FRAME_STUFF_ERR() : base(_message) { }

		public TKFRAME_CSAFE_FRAME_STUFF_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
