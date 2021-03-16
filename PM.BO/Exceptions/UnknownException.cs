using System;

namespace PM.BO.Exceptions
{
    public class UnknownException : Exception
    {
        private const string _message = "An unknown error code was encountered: ";

        public UnknownException(short code) : base(_message + code) { }
    }
}
