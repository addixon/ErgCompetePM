using System;

namespace PM.BO.Exceptions
{
	public class IOUSB_NULL_PTR_ERR : Exception
	{
		private const string _message = "Null USB pointer.";
		private const short Code = -935;

		public IOUSB_NULL_PTR_ERR() : base(_message) { }

		public IOUSB_NULL_PTR_ERR(string message) : base (_message + "\nAdditional Information: " + message) { }
	}
}
