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
        public RegisterWorkoutViewModel RegisterWorkoutVM { get; set; }
        public PreviousWorkoutsViewModel PreviousWorkoutsVM { get; set; }
        public EditPreviousWorkoutWrapper EditPreviousWrapper { get; set; }
        public EditPreviousWorkoutView EditPreviousWorkoutView { get; set; }
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
            PreviousWorkoutsVM = new PreviousWorkoutsViewModel(this);
            RegisterWorkoutVM = new RegisterWorkoutViewModel();
            EditPreviousWrapper = new EditPreviousWorkoutWrapper(PreviousWorkoutsVM, RegisterWorkoutVM);
            InstructionsView = new InstructionsView(this);
            MenuView = new MenuView(this);
            PreviousWorkoutsView = new PreviousWorkoutsView(this, PreviousWorkoutsVM);
            RegisterWorkoutView = new RegisterWorkoutView(this, RegisterWorkoutVM);
            TotalStatsView = new TotalStatsView(this);
            EditPreviousWorkoutView = new EditPreviousWorkoutView(this, PreviousWorkoutsVM, RegisterWorkoutVM);
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
                //if(view is EditPreviousWorkoutView)
                //{
                //    EditPreviousWorkoutView = new EditPreviousWorkoutView(this, PreviousWorkoutsVM, RegisterWorkoutVM);
                //    view = EditPreviousWorkoutView;
                //    CurrentView = view;
                //}
                CurrentView = view;
            }
        }
    }
}
