using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Labb2_WEWFY_Presentation.Commands;
using Labb2_WEWFY_Presentation.Views;

namespace Labb2_WEWFY_Presentation.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public InstructionsView InstructionsView { get; }
        public MenuView MenuView { get; }
        public PreviousWorkoutsView PreviousWorkoutsView { get; }
        public RegisterWorkoutView RegisterWorkoutView { get; }
        public TotalStatsView TotalStatsView { get; }
        public DelegateCommand ChangeCurrentViewCommand { get; }
        private UserControl? _currentView;

        public UserControl? CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                RaisePropertyChanged();
                ChangeCurrentViewCommand.RaiseCanExecuteChanged();

            }
        }

        public MainWindowViewModel()
        {
            InstructionsView = new InstructionsView(this);
            MenuView = new MenuView(this);
            PreviousWorkoutsView = new PreviousWorkoutsView(this);
            RegisterWorkoutView = new RegisterWorkoutView(this);
            TotalStatsView = new TotalStatsView(this);
            ChangeCurrentViewCommand = new DelegateCommand(ChangeCurrentView, CanChangeCurrentView);
            CurrentView = MenuView;
        }

        private bool CanChangeCurrentView(object? arg)
        {
            return arg != CurrentView;
        }

        private void ChangeCurrentView(object? obj)
        {
            if (obj is UserControl view)
            {
                CurrentView = view;
            }
        }
    }
}
