using System;

namespace PM.BO.Interfaces
{
    public interface IExceptionActivator
    {
        Exception CreateException(short errorCode);
    }
}
