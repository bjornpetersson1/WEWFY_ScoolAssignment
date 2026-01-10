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
using System.Windows;
using static Azure.Core.HttpHeader;

namespace Labb2_WEWFY_Presentation.ViewModels
{
    public class EditPreviousWorkoutViewModel: ViewModelBase
    {
        private string _exerciseDuration = "00:00:00";
        public string ExerciseDuration
        {
            get => _exerciseDuration;
            set
            {
                _exerciseDuration = value;
                RaisePropertyChanged();
                //AddExerciseCommand.RaiseCanExecuteChanged();
            }
        }

        private ObservableCollection<ExerciseLoggerViewModel> _currentWorkoutExercises = new();
        public ObservableCollection<ExerciseLoggerViewModel> CurrentWorkoutExercises
        {
            get => _currentWorkoutExercises;
            set
            {
                _currentWorkoutExercises = value;
                RaisePropertyChanged();
                //AddNewWorkoutCommand.RaiseCanExecuteChanged();
            }
        }

        private ExerciseLoggerViewModel _selectedWorkoutExercise;
        public ExerciseLoggerViewModel SelectedWorkoutExercise
        {
            get => _selectedWorkoutExercise;
            set
            {
                _selectedWorkoutExercise = value;
                RaisePropertyChanged();
                //RemoveExerciseCommand.RaiseCanExecuteChanged();
            }
        }

        private int? _waterBefore;
        public int? WaterBefore
        {
            get => _waterBefore;
            set { _waterBefore = value; RaisePropertyChanged(); }
        }

        private int? _waterDuring;
        public int? WaterDuring
        {
            get => _waterDuring;
            set { _waterDuring = value; RaisePropertyChanged(); }
        }

        private bool _fueling;
        public bool Fueling
        {
            get => _fueling;
            set { _fueling = value; RaisePropertyChanged(); }
        }

        private string? _notes;
        public string? Notes
        {
            get => _notes;
            set { _notes = value; RaisePropertyChanged(); }
        }

        private int _experienceRating = 3;
        public int ExperienceRating
        {
            get => _experienceRating;
            set { _experienceRating = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<string>? _exercises;
        public ObservableCollection<string>? Exercises
        {
            get => _exercises;
            set { _exercises = value; RaisePropertyChanged(); }
        }
        private string? _selectedExcersise;

        public string? SelectedExcersise
        {
            get => _selectedExcersise;
            set
            {
                _selectedExcersise = value;

                RaisePropertyChanged();
            }
        }
        private ObservableCollection<ExerciseLogger> _currentExercises;

        public ObservableCollection<ExerciseLogger> CurrentExercises
        {
            get { return _currentExercises; }
            set { _currentExercises = value; RaisePropertyChanged(); }
        }

        public PreviousWorkoutsViewModel PreviousWorkoutsVM { get; set; }
        public RegisterWorkoutViewModel RegisterVM { get; set; }
        public DelegateCommand UpdateWorkoutCommand { get; set; }
        public EditPreviousWorkoutViewModel(PreviousWorkoutsViewModel PreVM, RegisterWorkoutViewModel RegVM)
        {
            PreviousWorkoutsVM = PreVM;
            RegisterVM = RegVM;
            UpdateWorkoutCommand = new DelegateCommand(UpdateWorkout);
        }

        private void UpdateWorkout(object? obj)
        {
            EditWorkoutAsync();
        }

        public async Task LoadSelectedWorkoutData()
        {
            Notes = PreviousWorkoutsVM.SelectedWorkout.Notes;
            ExperienceRating = PreviousWorkoutsVM.SelectedWorkout.ExperienceRating;
            Fueling = PreviousWorkoutsVM.SelectedWorkout.Fueling;
            WaterDuring = PreviousWorkoutsVM.SelectedWorkout.WaterDuring;
            WaterBefore = PreviousWorkoutsVM.SelectedWorkout.WaterBefore;
            CurrentExercises = new ObservableCollection<ExerciseLogger>(PreviousWorkoutsVM.SelectedWorkout.ExerciseLoggers);
            await RegisterVM.LoadExcersisesAsync();
        }

        private async Task EditWorkoutAsync()
        {
            using var db = new WEWFYContext();

            var workout = await db.Workouts
                                  .Include(w => w.ExerciseLoggers)
                                  .ThenInclude(w => w.Exercise)
                                  .FirstOrDefaultAsync(w => w.Id == PreviousWorkoutsVM.SelectedWorkout.Id);
            if (workout == null)
                return;

            workout.LoggingDate = PreviousWorkoutsVM.SelectedWorkout.LoggingDate;
            workout.WaterBefore = WaterBefore;
            workout.WaterDuring = WaterDuring;
            workout.Fueling = Fueling;
            workout.Notes = Notes;
            workout.ExperienceRating = ExperienceRating;

            await db.SaveChangesAsync();

            db.ExerciseLoggers.RemoveRange(workout.ExerciseLoggers);

            foreach (var vm in PreviousWorkoutsVM.SelectedWorkout.ExerciseLoggers)
            {
                workout.ExerciseLoggers.Add(new ExerciseLogger
                {
                    WorkoutId = workout.Id,
                    ExerciseId = vm.ExerciseId,
                    Duration = vm.Duration
                });
            }


            await db.SaveChangesAsync();

        }
    }
}
