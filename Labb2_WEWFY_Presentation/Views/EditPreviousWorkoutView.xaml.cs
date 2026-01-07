using Labb2_WEWFY_Presentation.ViewModels;
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

namespace Labb2_WEWFY_Presentation.Views
{
    /// <summary>
    /// Interaction logic for EditPreviousWorkoutView.xaml
    /// </summary>
    public partial class EditPreviousWorkoutView : UserControl
    {
        public MainWindowViewModel MainVM { get; }
        public EditPreviousWorkoutView(
        MainWindowViewModel mainVM,
        PreviousWorkoutsViewModel prevVM,
        RegisterWorkoutViewModel regVM)
        {
            InitializeComponent();
            MainVM = mainVM;
            DataContext = new EditPreviousWorkoutWrapper(prevVM, regVM);
        }
    }
}
