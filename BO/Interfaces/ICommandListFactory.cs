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
    }
}
