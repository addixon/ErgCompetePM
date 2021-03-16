using BLL;
using BLL.Communication;
using BLL.Helpers;
using PM.BO.Enums;
using PM.BO.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using PM.BLL.Factories;
using PM.BO.Configuration;

namespace ErgCompetePM
{
    public class Startup
    {
        public static Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            return host.RunAsync();
        }


        /// <summary>
        /// The host builder
        /// </summary>
        /// <param name="args">Any arguments</param>
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            IHostBuilder hostBuilder =
                Host.CreateDefaultBuilder(args)
                    .ConfigureHostConfiguration(ConfigureHost)
                    .ConfigureLogging(ConfigureLogging)
                    .ConfigureServices(ConfigureServices);

            return hostBuilder;
        }

        private static void ConfigureLogging(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddConsole();
        }

        /// <summary>
        /// Configures the host itself
        /// </summary>
        /// <param name="configurationBuilder">The configuration builder</param>
        private static void ConfigureHost(IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configurationBuilder.AddJsonFile("appsettings.json", optional: true);
            configurationBuilder.AddEnvironmentVariables(prefix: "PREFIX_");
            configurationBuilder.AddCommandLine(Array.Empty<string>());
        }

        /// <summary>
        /// Configures services
        /// </summary>
        /// <param name="hostBuilderContext">The host context</param>
        /// <param name="serviceCollection">The service collection</param>
        private static void ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection serviceCollection)
        {
            serviceCollection.AddLogging();

            // Add SignalR
            serviceCollection.AddSignalR();

            // Add the hosted service (runtime)
            serviceCollection.AddHostedService<Program>();

            // Add injected dependencies
            serviceCollection.AddScoped<IPMService, PMService>();
            serviceCollection.AddScoped<IResponseReader, ResponseReader>();
            serviceCollection.AddSingleton<IPMCommunicator, PMCommunicator>();
            serviceCollection.AddScoped<IPMPollingService, PMPollingService>();
            serviceCollection.AddTransient<ICommandListFactory, CommandListFactory>();

            serviceCollection.AddSingleton<ILoggerFactory, LoggerFactory>();

            // Add configuration
            serviceCollection.Configure<ProgramConfiguration>(hostBuilderContext.Configuration.GetSection("Program"));
        }
    }
}
