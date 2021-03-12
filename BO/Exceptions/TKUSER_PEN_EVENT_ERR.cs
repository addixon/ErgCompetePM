using System;

namespace BO.Exceptions
{
	public class TKUSER_PEN_EVENT_ERR : Exception
	{
		private const string _message = "Pen event error.";
		private const short Code = -282;

		public TKUSER_PEN_EVENT_ERR() : base(_message) { }

		public TKUSER_PEN_EVENT_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
