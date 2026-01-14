using Labb2_WEWFY_Domain;
using Labb2_WEWFY_Infrastructure.Data.Model;
using Labb2_WEWFY_Presentation.Commands;
using Labb2_WEWFY_Presentation.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_WEWFY_Presentation.ViewModels
{
    public class PreviousWorkoutsViewModel : ViewModelBase
    {
        private bool _showDeleteConfirmation;
        public bool ShowDeleteConfirmation
        {
            get => _showDeleteConfirmation;
            set
            {
                _showDeleteConfirmation = value;
                RaisePropertyChanged();
                DeleteWorkoutCommand.RaiseCanExecuteChanged();
                CancelDeleteCommand.RaiseCanExecuteChanged();
            }

        }
        public DelegateCommand ShowDeleteConfirmationCommand { get; }
        public DelegateCommand CancelDeleteCommand { get; }

        public DelegateCommand DeleteWorkoutCommand { get; set; }
        public bool HaveSelectedWorkout => SelectedWorkout != null;
        private ObservableCollection<Workout> workouts;
        public ObservableCollection<Workout> Workouts 
        {
            get => workouts;
            set
            {
                workouts = value;
                RaisePropertyChanged();
            }
        }

        private Workout? _selectedWorkout;
        public Workout? SelectedWorkout
        {
            get => _selectedWorkout;
            set
            {
                _selectedWorkout = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(HaveSelectedWorkout));
                ShowDeleteConfirmationCommand.RaiseCanExecuteChanged();
                DeleteWorkoutCommand.RaiseCanExecuteChanged();

            }
        }

        private readonly LoadPreviousWorkoutsService _LoadPreviousWorkoutsService;
        public MainWindowViewModel MainVM { get; }
        public PreviousWorkoutsViewModel(MainWindowViewModel mainWindow)
        {
            DeleteWorkoutCommand = new DelegateCommand(DeleteWorkout, CanDeleteWorkout);
            ShowDeleteConfirmationCommand = new DelegateCommand(OpenDeleteCofirmation, CanShowDeleteConfirmation);
            CancelDeleteCommand = new DelegateCommand(CancelDelete, CanCancelDelete);
            _LoadPreviousWorkoutsService = new LoadPreviousWorkoutsService();
            MainVM = mainWindow;
        }

        private void OpenDeleteCofirmation(object? obj)
        {
            ShowDeleteConfirmation = true;
        }

        private bool CanShowDeleteConfirmation(object? arg)
        {
            return SelectedWorkout != null;
        }

        private void CancelDelete(object? obj)
        {
            ShowDeleteConfirmation = false;
        }

        private bool CanCancelDelete(object? arg)
        {
            return ShowDeleteConfirmation;
        }

        private bool CanDeleteWorkout(object? arg)
        {
            return ShowDeleteConfirmation && SelectedWorkout != null;
        }

        private void DeleteWorkout(object? obj)
        {
            _ = DeleteWorkoutAsync(obj);
            ShowDeleteConfirmation = false;
        }
        private async Task DeleteWorkoutAsync(object? obj)
        {

            using var db = new WEWFYContext();

            var workout = await db.Workouts
                                  .Include(w => w.ExerciseLoggers)
                                  .FirstOrDefaultAsync(w => w.Id == SelectedWorkout.Id);

            if (workout == null)
                return;
            db.Workouts.Remove(workout);
            await db.SaveChangesAsync();

            Workouts.Remove(SelectedWorkout);

            SelectedWorkout = null;

            MainVM.IsTestdataAdded = await db.Workouts.AnyAsync(w => w.IsTestData);
        }

        public async Task LoadPreviousWorkoutsAsync()
        {
            var workouts = await _LoadPreviousWorkoutsService.GetPreviousWorkoutsAsync();
            Workouts = new ObservableCollection<Workout>(workouts);
        }
    }
}
