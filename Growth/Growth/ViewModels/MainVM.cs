using Microsoft.Extensions.Logging;

namespace Growth.ViewModels;

public class MainVM : ViewModelBase
{
    private readonly ILogger _logger;
    public MainVM(ILogger<MainVM> logger)
    {
        _logger = logger;
        _logger.LogInformation("MainVM Constructed Successfully.");
    }
}
