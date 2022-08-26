using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Windows;

using Growth.Utilities;
using Growth.ViewModels;
using DataAccess;

namespace Growth;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IHost? _host { get; private set; }
    private readonly ILogger<App> _logger;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainVM>();
                services.AddTransient<IDataAccess, SQLiteDataAccess>();
            })
            .ConfigureLogging((context, logging) =>
            {
                logging.ClearProviders();
                logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                logging.AddDebug();
                logging.AddConsole().AddConsoleFormatter<TimePrefixConsoleFormatter, ConsoleFormatterOptionsWrapper>();
            })
            .Build();

        // Add first log
        ILoggerFactory loggerFactory = _host.Services.GetRequiredService<ILoggerFactory>();
        _logger = loggerFactory.CreateLogger<App>();

        using (_logger.BeginScope("Logging scope"))
            _logger.LogInformation("Exiting App Constructor!");
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await _host!.StartAsync();

        var startupWindow = _host.Services.GetRequiredService<MainWindow>();
        startupWindow.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _host!.StopAsync();
        base.OnExit(e);
    }

    private void OnAppUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        string exMsg = $"An unhandled exception occured: {e.Exception.Message}";
        _logger.LogError(exMsg, sender.GetType());
        MessageBox.Show(exMsg);
        e.Handled = true;
    }
}
