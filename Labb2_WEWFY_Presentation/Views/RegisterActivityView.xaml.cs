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
    /// Interaction logic for RegisterActivity.xaml
    /// </summary>
    public partial class RegisterWorkoutView : UserControl
    {
        public MainWindowViewModel MainVM { get; }
        public RegisterWorkoutView(MainWindowViewModel mainVM)
        {
            InitializeComponent();
            MainVM = mainVM;
            DataContext = new RegisterWorkoutViewModel();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterWorkoutViewModel vm)
            {
                await vm.LoadExcersisesAsync();
            }
        }

        private async void AddWorkout_Click(object sender, RoutedEventArgs e)
        {
            addWorkout.IsEnabled = false;
            await Task.Delay(2000);
            MainVM.CurrentView = MainVM.MenuView;
            addWorkout.IsEnabled = true;
        }
    }
}
