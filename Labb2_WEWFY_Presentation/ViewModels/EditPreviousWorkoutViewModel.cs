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
                AddExerciseCommand.RaiseCanExecuteChanged();
            }
        }

        private ExerciseLogger _selectedWorkoutExercise;
        public ExerciseLogger SelectedWorkoutExercise
        {
            get => _selectedWorkoutExercise;
            set
            {
                _selectedWorkoutExercise = value;
                RaisePropertyChanged();
                RemoveExerciseCommand.RaiseCanExecuteChanged();
            }
        }

        private int? _waterBefore;
        public int? WaterBefore
        {
            get => _waterBefore;
            set 
            {
                if (value > 3000) value = 3000;
                _waterBefore = value; 
                RaisePropertyChanged();
            }
        }

        private int? _waterDuring;
        public int? WaterDuring
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
        private ObservableCollection<ExerciseLogger> _currentExercises;

        public ObservableCollection<ExerciseLogger> CurrentExercises
        {
            get { return _currentExercises; }
            set 
            {
                _currentExercises = value; 
                RaisePropertyChanged();
                UpdateWorkoutCommand.RaiseCanExecuteChanged();
            }
        }

        public PreviousWorkoutsViewModel PreviousWorkoutsVM { get; set; }
        public RegisterWorkoutViewModel RegisterVM { get; set; }
        public DelegateCommand UpdateWorkoutCommand { get; set; }
        public DelegateCommand AddExerciseCommand { get; set; }
        public DelegateCommand RemoveExerciseCommand { get; set; }
        public EditPreviousWorkoutViewModel(PreviousWorkoutsViewModel PreVM, RegisterWorkoutViewModel RegVM)
        {
            PreviousWorkoutsVM = PreVM;
            RegisterVM = RegVM;
            AddExerciseCommand = new DelegateCommand(AddExercise, CanAddExercise);
            UpdateWorkoutCommand = new DelegateCommand(UpdateWorkout, CanUpdateWorkout);
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

            CurrentExercises.Remove(SelectedWorkoutExercise);
            SelectedWorkoutExercise = null;
        }

        private bool CanUpdateWorkout(object? arg)
        {
            return CurrentExercises != null && CurrentExercises.Count > 0;
        }

        private async void UpdateWorkout(object? obj)
        {
            await EditWorkoutAsync();
        }
        private bool CanAddExercise(object? arg)
        {
            if (!TimeSpan.TryParse(ExerciseDuration, out var duration))
                return false;

            return duration.TotalDays < 1 && duration > TimeSpan.Zero && TryGetExerciseDuration(out _);
        }
        private async void AddExercise(object? obj)
        {
            if (string.IsNullOrWhiteSpace(SelectedExcersise))
                return;

            if (!TryGetExerciseDuration(out var duration))
                return;


            using var db = new WEWFYContext();

            var exercise = await db.Exercises
                .FirstOrDefaultAsync(e => e.ExerciseName == SelectedExcersise);


            if (exercise == null)
                return;

            CurrentExercises.Add(new ExerciseLogger
            {
                ExerciseId = exercise.Id,
                Exercise = exercise,
                Duration = duration
            });

            ExerciseDuration = "00:00:00";

         
        }

        public async Task LoadSelectedWorkoutData()
        {
            Notes = PreviousWorkoutsVM.SelectedWorkout.Notes;
            ExperienceRating = PreviousWorkoutsVM.SelectedWorkout.ExperienceRating;
            Fueling = PreviousWorkoutsVM.SelectedWorkout.Fueling;
            WaterDuring = PreviousWorkoutsVM.SelectedWorkout.WaterDuring;
            WaterBefore = PreviousWorkoutsVM.SelectedWorkout.WaterBefore;
            CurrentExercises = new ObservableCollection<ExerciseLogger>(PreviousWorkoutsVM.SelectedWorkout.ExerciseLoggers);
            Exercises = await RegisterVM.LoadExercisesAsync();
            SelectedExcersise = Exercises.FirstOrDefault();
            CurrentExercises.CollectionChanged += (s, e) => UpdateWorkoutCommand.RaiseCanExecuteChanged();
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

            foreach (var vm in CurrentExercises)
            {
                workout.ExerciseLoggers.Add(new ExerciseLogger
                {
                    WorkoutId = workout.Id,
                    ExerciseId = vm.ExerciseId,
                    Duration = vm.Duration
                });
            }

            await db.SaveChangesAsync();
            CurrentExercises.Clear();
            RegisterVM.ShowSuccesfulSave();
        }
    }
}
