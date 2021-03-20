using System.Collections.Generic;

namespace PM.BO.Interfaces
{
    public interface ICommandList : IList<ICommand>
    {
        /// <summary>
        /// The total number of prepared frames
        /// </summary>
        int TotalFrames { get; }

        /// <summary>
        /// Checks if the command list is in a state that can be sent
        /// </summary>
        bool CanSend { get; }

        /// <summary>
        /// Resets and clears the command list
        /// </summary>
        void Reset();

        /// <summary>
        /// Adds a new command to the list
        /// </summary>
        /// <param name="command">The command to add</param>
        new void Add(ICommand command);

        /// <summary>
        /// Adds multiple commands to the list
        /// </summary>
        /// <param name="commands">The commands to add</param>
        void AddRange(IEnumerable<ICommand> commands);

        /// <summary>
        /// Gets a frame
        /// </summary>
        /// <param name="frameNumber">The frame number to get</param>
        /// <returns>The bytes in the frame</returns>
        uint[] GetFrame(int frameNumber);

        /// <summary>
        /// Prepares the list for communication
        /// </summary>
        /// <remarks>
        /// This must be performed prior to submitting the list to be sent
        /// </remarks>
        void Prepare();

        /// <summary>
        /// Reads data from the response reader into the commands in the list
        /// </summary>
        /// <param name="reader">The response reader</param>
        /// <returns>True if all commands were read successfully</returns>
        bool Read(IResponseReader reader);
    }
}
