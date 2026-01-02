using Labb2_WEWFY_Domain;
using Labb2_WEWFY_Infrastructure.Data.Model;
using Labb2_WEWFY_Presentation.Commands;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Labb2_WEWFY_Presentation.ViewModels
{
    class RegisterWorkoutViewModel : ViewModelBase
    {
        public DelegateCommand AddNewWorkoutCommand { get; set; }
        public DelegateCommand AddExerciseCommand { get; set; }
        public DelegateCommand RemoveExerciseCommand { get; set; }
        private string _exerciseDuration = "00:00:00";
        public string ExerciseDuration
        {
            get => _exerciseDuration;
            set { _exerciseDuration = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<ExerciseLoggerViewModel> _currentWorkoutExercises = new();
        public ObservableCollection<ExerciseLoggerViewModel> CurrentWorkoutExercises
        {
            get => _currentWorkoutExercises;
            set { _currentWorkoutExercises = value; RaisePropertyChanged(); }
        }

        private ExerciseLoggerViewModel _selectedWorkoutExercise;
        public ExerciseLoggerViewModel SelectedWorkoutExercise
        {
            get => _selectedWorkoutExercise;
            set
            {
                _selectedWorkoutExercise = value;
                RaisePropertyChanged();
                RemoveExerciseCommand.RaiseCanExecuteChanged();
            }
        }

        private int _waterBefore;
        public int WaterBefore
        {
            get => _waterBefore;
            set { _waterBefore = value; RaisePropertyChanged(); }
        }

        private int _waterDuring;
        public int WaterDuring
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
        public RegisterWorkoutViewModel()
        {
            AddNewWorkoutCommand = new DelegateCommand(AddNewWorkout);
            AddExerciseCommand = new DelegateCommand(AddExercise);
            RemoveExerciseCommand = new DelegateCommand(RemoveExercise, CanRemoveExercise);
        }

        private bool CanRemoveExercise(object? arg)
        {
            return SelectedWorkoutExercise != null;
        }

        private void RemoveExercise(object? obj)
        {
            if (SelectedWorkoutExercise == null)
                return;

            CurrentWorkoutExercises.Remove(SelectedWorkoutExercise);
            SelectedWorkoutExercise = null;
        }

        private async void AddExercise(object? obj)
        {
            if (string.IsNullOrWhiteSpace(SelectedExcersise))
                return;

            if (!TimeSpan.TryParse(ExerciseDuration, out var duration))
                return;

            using var db = new WEWFYContext();

            var exercise = await db.Exercises
                .Where(e => e.ExerciseName == SelectedExcersise)
                .Select(e => new { e.Id, e.ExerciseName })
                .FirstOrDefaultAsync();

            if (exercise == null)
                return;

            CurrentWorkoutExercises.Add(new ExerciseLoggerViewModel
            {
                ExerciseId = exercise.Id,
                ExerciseName = exercise.ExerciseName,
                Duration = duration
            });

            ExerciseDuration = "00:00:00";
        }

        private void AddNewWorkout(object? obj)
        {
            CreateNewWorkoutAsync();
        }

        public async Task LoadExcersisesAsync()
        {
            using var db = new WEWFYContext();
            Exercises = new ObservableCollection<string>(
                await db.Exercises.Select(e => e.ExerciseName).ToListAsync()
            );

            SelectedExcersise = Exercises.FirstOrDefault();
        }
        private async void CreateNewWorkoutAsync()
        {
            using var db = new WEWFYContext();

            var workout = new Workout
            {
                LoggingDate = DateTime.Now,
                WaterBefore = WaterBefore,
                WaterDuring = WaterDuring,
                Fueling = Fueling,
                Notes = Notes,
                ExperienceRating = ExperienceRating,
                ExerciseLoggers = new List<ExerciseLogger>()
            };

            db.Workouts.Add(workout);
            await db.SaveChangesAsync();

            foreach (var vm in CurrentWorkoutExercises)
            {
                db.ExerciseLoggers.Add(new ExerciseLogger
                {
                    WorkoutId = workout.Id,
                    ExerciseId = vm.ExerciseId,
                    Duration = vm.Duration
                });
            }

            await db.SaveChangesAsync();
            CurrentWorkoutExercises.Clear();
        }
    }
}
