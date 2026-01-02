using Labb2_WEWFY_Domain;
using Labb2_WEWFY_Infrastructure.Data.Model;
using Labb2_WEWFY_Presentation.Commands;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Labb2_WEWFY_Presentation.ViewModels
{
    class RegisterWorkoutViewModel : ViewModelBase
    {

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
        public DelegateCommand AddNewWorkoutCommand { get; set; }

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

            db.Add(workout);
            await db.SaveChangesAsync();
        }
    }
}
