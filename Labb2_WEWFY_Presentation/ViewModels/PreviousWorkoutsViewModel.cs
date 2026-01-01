using Labb2_WEWFY_Domain;
using Labb2_WEWFY_Infrastructure.Data.Model;
using Labb2_WEWFY_Presentation.Commands;
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
        //public DelegateCommand ShowWorkoutDetailsCommand { get; set; }
        //public Action<object?> ShowWorkoutDetails { get; set; }
        //public ObservableCollection<Workout>? SelectedWorkoutExercises
        //{
        //    get => SelectedWorkout? is null
        //        ? null
        //        : new ObservableCollection<Workout>(SelectedWorkout);
        //}

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
                //ShowWorkoutDetailsCommand.RaiseCanExecuteChanged();
            }
        }
        public PreviousWorkoutsViewModel()
        {
            LoadPreviousWorkoutsAsync();
            //ShowWorkoutDetailsCommand = new DelegateCommand(DoShowWorkoutDetails, CanShowWorkoutDetails);
        }

        private bool CanShowWorkoutDetails(object? arg)
        {
            return SelectedWorkout is not null;
        }

        private void DoShowWorkoutDetails(object? obj)
        {
            //ShowWorkoutDetails(obj);
        }

        private async void LoadPreviousWorkoutsAsync()
        {
            using var db = new WEWFYContext();
            Workouts = new ObservableCollection<Workout>(await db.Workouts
                                                                .Include(w => w.ExerciseLoggers)
                                                                .ToListAsync());
        }
    }
}
