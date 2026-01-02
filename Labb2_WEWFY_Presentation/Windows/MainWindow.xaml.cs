using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Labb2_WEWFY_Infrastructure.Data.Model;
using Labb2_WEWFY_Presentation.ViewModels;

namespace Labb2_WEWFY_Presentation.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            using var db = new WEWFYContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void Button_Quit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}