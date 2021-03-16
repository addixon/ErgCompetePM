namespace PM.BO.Interfaces
{
    /// <summary>
    /// Interface for the Response Reader
    /// </summary>
    public interface IResponseReader : ICommunicationBuffer<uint>
    {
        /// <summary>
        /// Reads a byte from the buffer
        /// </summary>
        /// <returns>The byte as a uint</returns>
        uint ReadByte();

        /// <summary>
        /// Reads a ushort from the buffer
        /// </summary>
        /// <returns>The ushort as a uint</returns>
        uint ReadUShort();

        /// <summary>
        /// Reads a uint from the buffer
        /// </summary>
        /// <returns>The uint</returns>
        uint ReadUInt();

        /// <summary>
        /// Reads the specified number of bytes from the buffer
        /// </summary>
        /// <param name="totalBytes">The number of bytes to read</param>
        /// <returns>The bytes as a uint</returns>
        uint ReadBytes(int totalBytes);

        /// <summary>
        /// Reads the specified number of bytes from the buffer
        /// </summary>
        /// <typeparam name="TReturnType">The return type</typeparam>
        /// <param name="totalBytes">The number of bytes to read</param>
        /// <returns>The bytes</returns>
        TReturnType ReadBytes<TReturnType>(int totalBytes) where TReturnType : struct;

        /// <summary>
        /// Truncates the reader
        /// </summary>
        /// <param name="index">The index at which to truncate</param>
        void Truncate(int index);
    }
}
