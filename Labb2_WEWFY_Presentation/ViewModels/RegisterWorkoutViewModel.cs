using Labb2_WEWFY_Domain;
using Labb2_WEWFY_Infrastructure.Data.Model;
using Labb2_WEWFY_Presentation.Commands;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Labb2_WEWFY_Presentation.ViewModels
{
    public class RegisterWorkoutViewModel : ViewModelBase
    {
        public DelegateCommand AddNewWorkoutCommand { get; set; }
        public DelegateCommand AddExerciseCommand { get; set; }
        public DelegateCommand RemoveExerciseCommand { get; set; }
        private bool _isMessageVisible;
        public bool IsMessageVisible
        {
            get => _isMessageVisible;
            set
            {
                _isMessageVisible = value;
                RaisePropertyChanged();
            }
        }
        private string _exerciseDuration = "00:00:00";
        public string ExerciseDuration
        {
            get => _exerciseDuration;
            set
            {
                _exerciseDuration = value;
                RaisePropertyChanged();
                AddExerciseCommand.RaiseCanExecuteChanged();
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
                AddNewWorkoutCommand.RaiseCanExecuteChanged();
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
                RemoveExerciseCommand.RaiseCanExecuteChanged();
            }
        }

        private int _waterBefore;
        public int WaterBefore
        {
            get => _waterBefore;
            set 
            {
                if (value > 3000) value = 3000;
                _waterBefore = value; 
                RaisePropertyChanged(); 
            }
        }

        private int _waterDuring;
        public int WaterDuring
        {
            get => _waterDuring;
            set 
            {
                if (value > 3000) value = 3000;
                _waterDuring = value; 
                RaisePropertyChanged(); 
            }
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
            set 
            {
                if (value == null)
                {
                    _notes = null;
                }
                else
                {
                    _notes = value.Length > 500
                        ? value.Substring(0, 500)
                        : value;
                }
                RaisePropertyChanged(); 
            }
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
            AddNewWorkoutCommand = new DelegateCommand(AddNewWorkout, CanAddNewWorkout);
            AddExerciseCommand = new DelegateCommand(AddExercise, CanAddExercise);
            RemoveExerciseCommand = new DelegateCommand(RemoveExercise, CanRemoveExercise);
        }

        private bool TryGetExerciseDuration(out TimeSpan duration)
        {
            duration = TimeSpan.Zero;

            if (!TimeSpan.TryParse(ExerciseDuration, out var parsed))
                return false;

            var max = new TimeSpan(23, 59, 59);

            if (parsed < TimeSpan.Zero || parsed > max)
                return false;

            duration = parsed;
            return true;
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
            AddNewWorkoutCommand.RaiseCanExecuteChanged();
        }

        private bool CanAddExercise(object? arg)
        {
            return TryGetExerciseDuration(out _);
        }
        private async void AddExercise(object? obj)
        {
            if (string.IsNullOrWhiteSpace(SelectedExcersise))
                return;

            if (!TryGetExerciseDuration(out var duration))
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
            AddNewWorkoutCommand.RaiseCanExecuteChanged();
        }

        private bool CanAddNewWorkout(object? arg)
        {
            return CurrentWorkoutExercises.Count != 0;
        }
        private void AddNewWorkout(object? obj)
        {
            CreateNewWorkoutAsync();
        }

        public async Task<ObservableCollection<string>> LoadExercisesAsync()
        {
            using var db = new WEWFYContext();

            return new ObservableCollection<string>(
                await db.Exercises
                        .Select(e => e.ExerciseName)
                        .ToListAsync()
            );
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
            ShowSuccesfulSave();
        }
        public void ResetForm()
        {
            ExerciseDuration = "00:00:00";

            CurrentWorkoutExercises.Clear();

            WaterBefore = 0;
            WaterDuring = 0;
            Fueling = false;

            Notes = string.Empty;
            ExperienceRating = 3;

            SelectedWorkoutExercise = null;
            IsMessageVisible = false;
            AddNewWorkoutCommand.RaiseCanExecuteChanged();
        }

        public async void ShowSuccesfulSave()
        {
            IsMessageVisible = true;
            await Task.Delay(3000);
            IsMessageVisible = false;
        }
        public async Task InitializeAsync()
        {
            Exercises = await LoadExercisesAsync();
            SelectedExcersise = Exercises.FirstOrDefault();
        }

    }
}
