using System;

namespace PM.BO.Interfaces
{
    /// <summary>
    /// Interface for ExceptionActivator
    /// </summary>
    public interface IExceptionActivator
    {
        /// <summary>
        /// Create a specific exception based on the error code
        /// </summary>
        /// <param name="errorCode">The error code</param>
        /// <returns>The exception</returns>
        Exception CreateException(short errorCode);
    }
}
