using System;

namespace BO.Exceptions
{
	public class TKFRAME_NO_SCI_FRAME_ERR : Exception
	{
		private const string _message = "A valid frame was not received on the SCI interface.";
		private const short Code = -162;

		public TKFRAME_NO_SCI_FRAME_ERR() : base(_message) { }

		public TKFRAME_NO_SCI_FRAME_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
