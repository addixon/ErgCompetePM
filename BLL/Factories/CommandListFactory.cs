using BLL.Communication;
using Microsoft.Extensions.Logging;
using PM.BO.Interfaces;

namespace PM.BLL.Factories
{
    public class CommandListFactory : ICommandListFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<CommandListFactory> _logger;
        private readonly ILogger<CommandList> _commandListLogger;

        public CommandListFactory(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CommandListFactory>();
            _loggerFactory = loggerFactory;
            _commandListLogger = _loggerFactory.CreateLogger<CommandList>();
        }

        public ICommandList Create()
        {
            return new CommandList(_commandListLogger);
        }
    }
}
