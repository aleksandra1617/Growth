using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Windows;
using Growth.Utilities;
using System;
using System.Text;

namespace Growth
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        private readonly ILogger<App> _logger;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                    logging.AddDebug();
                    logging.AddConsole().AddConsoleFormatter<TimePrefixConsoleFormatter, ConsoleFormatterOptionsWrapper>();
                })
                .Build();

            ILoggerFactory loggerFactory = _host.Services.GetRequiredService<ILoggerFactory>();
            _logger = loggerFactory.CreateLogger<App>();

            using (_logger.BeginScope("Logging scope"))
                _logger.LogInformation("Exiting App Constructor!");
        }

        private void OnAppUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string exMsg = $"An unhandled exception occured: {e.Exception.Message}";
            _logger.LogError(exMsg, sender.GetType());
            MessageBox.Show(exMsg);
            e.Handled = true;
        }
    }
}
