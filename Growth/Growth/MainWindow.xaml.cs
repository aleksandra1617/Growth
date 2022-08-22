using System.Windows;

namespace Growth
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(ViewModels.MainVM mainVM)
        {
            InitializeComponent();

            DataContext = mainVM;
        }
    }
}
