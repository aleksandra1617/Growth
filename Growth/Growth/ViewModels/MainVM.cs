using DataAccess;
using Microsoft.Extensions.Logging;

namespace Growth.ViewModels;

public class MainVM : ViewModelBase
{
    #region Private
    private readonly ILogger _logger;
    private readonly IDataAccess _dataAccess;
    #endregion

    public MainVM(ILogger<MainVM> logger, IDataAccess dataAccess)
    {
        _logger = logger;
        _dataAccess = dataAccess;


        _logger.LogInformation("<<MainVM>> Constructed Successfully.");
    }
}
