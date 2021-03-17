using System.Collections.Generic;

namespace PM.BO.Interfaces
{
    /// <summary>
    /// Interface for CommandListFactory
    /// </summary>
    public interface ICommandListFactory
    {
        /// <summary>
        /// Creates a new CommandList
        /// </summary>
        /// <returns>The interfaced CommandList</returns>
        ICommandList Create();

        /// <summary>
        /// Creates a new CommandList from the provided commands
        /// </summary>
        /// <param name="commands">The commands</param>
        /// <returns>The interfaced CommandList</returns>
        ICommandList Create(IEnumerable<ICommand> commands);
    }
}
