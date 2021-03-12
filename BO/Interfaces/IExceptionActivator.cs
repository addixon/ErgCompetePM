using System;

namespace BO.Interfaces
{
    public interface IExceptionActivator
    {
        Exception CreateException(short errorCode);
    }
}
