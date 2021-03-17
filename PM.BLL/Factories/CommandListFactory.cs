using BLL.Communication;
using Microsoft.Extensions.Logging;
using PM.BO.Interfaces;
using System.Collections.Generic;

namespace PM.BLL.Factories
{
    /// <summary>
    /// Generates CommandLists on demand with appropriate logging
    /// </summary>
    public class CommandListFactory : ICommandListFactory
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<CommandListFactory> _logger;

        /// <summary>
        /// The logger for CommandList
        /// </summary>
        private readonly ILogger<CommandList> _commandListLogger;

        /// <summary>
        /// DI Constructor
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        public CommandListFactory(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CommandListFactory>();
            _commandListLogger = loggerFactory.CreateLogger<CommandList>();
        }

        /// <inheritdoc />
        public ICommandList Create()
        {
            return new CommandList(_commandListLogger);
        }

        /// <inheritdoc />
        public ICommandList Create(IEnumerable<ICommand> commands)
        {
            ICommandList commandList = new CommandList(_commandListLogger);
            commandList.AddRange(commands);
            return commandList;
        }
    }
}
