using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Windows;
using Growth.Utilities;


namespace Growth
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            using IHost host = Host.CreateDefaultBuilder()
           .ConfigureLogging(builder =>
           {
               builder.AddConsole().AddConsoleFormatter<TimePrefixConsoleFormatter, ConsoleFormatterOptionsWrapper>();
           })
           .Build();

            ILoggerFactory loggerFactory = host.Services.GetRequiredService<ILoggerFactory>();
            ILogger<App> logger = loggerFactory.CreateLogger<App>();

            using (logger.BeginScope("Logging scope"))
                logger.LogInformation("Exiting App Constructor!");
        }
    }
}
