using System;

namespace BO.Exceptions
{
	public class IORTCLOCK_WRITE_TIME_ERR : Exception
	{
		private const string _message = "Realtime clock write error.";
		private const short Code = -880;

		public IORTCLOCK_WRITE_TIME_ERR() : base(_message) { }

		public IORTCLOCK_WRITE_TIME_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
