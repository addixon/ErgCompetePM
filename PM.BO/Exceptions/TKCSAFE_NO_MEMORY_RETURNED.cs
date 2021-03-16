using System;

namespace PM.BO.Exceptions
{
	public class TKCSAFE_NO_MEMORY_RETURNED : Exception
	{
		private const string _message = "PM3 returned no memory data..";
		private const short Code = -10187;

		public TKCSAFE_NO_MEMORY_RETURNED() : base(_message) { }

		public TKCSAFE_NO_MEMORY_RETURNED(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
