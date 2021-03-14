using System;

namespace PM.BO.Exceptions
{
    public class UnsupportedDeviceException : Exception
    {
        public UnsupportedDeviceException()
        {

        }

        public UnsupportedDeviceException(string message) : base(message)
        {

        }
    }
}
