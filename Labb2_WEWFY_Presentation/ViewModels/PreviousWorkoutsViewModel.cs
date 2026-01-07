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
    class PreviousWorkoutsViewModel : ViewModelBase
    {
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
                DeleteWorkoutCommand.RaiseCanExecuteChanged();
            }
        }

        private readonly LoadPreviousWorkoutsService _LoadPreviousWorkoutsService;
        public MainWindowViewModel MainVM { get; }
        public PreviousWorkoutsViewModel(MainWindowViewModel mainWindow)
        {
            DeleteWorkoutCommand = new DelegateCommand(DeleteWorkout, CanDeleteWorkout);
            _LoadPreviousWorkoutsService = new LoadPreviousWorkoutsService();
            MainVM = mainWindow;
        }

        private bool CanDeleteWorkout(object? arg)
        {
            return SelectedWorkout != null;
        }

        private void DeleteWorkout(object? obj)
        {
            _ = DeleteWorkoutAsync(obj);
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
        }


        //public async Task OnNavigatedToAsync()
        //{
        //    await LoadPreviousWorkoutsAsync();
        //}

        public async Task LoadPreviousWorkoutsAsync()
        {
            var workouts = await _LoadPreviousWorkoutsService.GetPreviousWorkoutsAsync();
            Workouts = new ObservableCollection<Workout>(workouts);
        }
    }
}
