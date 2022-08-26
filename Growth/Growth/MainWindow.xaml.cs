using DataAccess;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace Growth
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILogger _logger;
        private readonly IDataAccess _dataAccess;

        public MainWindow(ILogger<MainWindow> logger, ViewModels.MainVM mainVM, IDataAccess dataAccess)
        {
            InitializeComponent();
            DataContext = mainVM;

            _logger = logger;
            _dataAccess = dataAccess;

            _logger.LogDebug("<<MainWindow>> Constructed Successfully.");
        }

        private void GetDataBtn_Click(object sender, RoutedEventArgs e)
        {
            _dataAccess.Connect();
            TestDataBlock.Text = _dataAccess.GetData("Roles");
            _logger.LogDebug(Utilities.Log.GetFormattedCallInfo());
        }
    }
}
