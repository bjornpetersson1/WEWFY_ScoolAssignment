using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Labb2_WEWFY_Presentation.ViewModels;

namespace Labb2_WEWFY_Presentation.Views
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        public MainWindowViewModel MainVM { get; }
        public MenuView(MainWindowViewModel mainVM)
        {
            InitializeComponent();
            MainVM = mainVM;
            DataContext = this;
        }

        private void Button_Quit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
