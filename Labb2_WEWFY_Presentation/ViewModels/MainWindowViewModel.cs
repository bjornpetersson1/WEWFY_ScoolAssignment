using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Labb2_WEWFY_Presentation.Views;

namespace Labb2_WEWFY_Presentation.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        public InstructionsView InstructionsView { get; }
        public MenuView MenuView { get; }
        public PreviousWorkoutsView PreviousWorkoutsView { get; }
        public RegisterActivityView RegisterActivityView { get; }
        public TotalStatsView TotalStatsView { get; }
        private UserControl? _currentView;

        public UserControl? CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                RaisePropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            InstructionsView = new InstructionsView();
            MenuView = new MenuView();
            PreviousWorkoutsView = new PreviousWorkoutsView();
            RegisterActivityView = new RegisterActivityView();
            TotalStatsView = new TotalStatsView();

            CurrentView = RegisterActivityView;
        }
    }
}
