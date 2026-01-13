using Labb2_WEWFY_Domain;
using Labb2_WEWFY_Infrastructure.Data.Model;
using Labb2_WEWFY_Presentation.Commands;
using Labb2_WEWFY_Presentation.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
        public DelegateCommand AddTestDataCommand { get; }
        private bool _isTestdataAdded = false;

        public bool IsTestdataAdded
        {
            get { return _isTestdataAdded; }
            set 
            {
                _isTestdataAdded = value;
                AddTestDataCommand.RaiseCanExecuteChanged();
            }
        }

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
            AddTestDataCommand = new DelegateCommand(AddTestData, CanAddTestData);
            CurrentView = MenuView;
        }

        private bool CanAddTestData(object? arg)
        {
            return IsTestdataAdded != true;
        }

        private async void AddTestData(object? obj)
        {
            await AddTestDataToDataBase();
            IsTestdataAdded = true;
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
        private async Task AddTestDataToDataBase()
        {
            using var db = new WEWFYContext();

            var testDataWorkouts = new List<Workout>
            {
                new Workout
                {
                    //Id = 1,
                    Fueling = true,
                    ExperienceRating = 3,
                    WaterBefore = 330,
                    WaterDuring = 0,
                    LoggingDate = new DateTime(2025, 12, 01),
                    Notes = "Spydde, annars bra"
                },
                new Workout
                {
                    //Id = 2,
                    Fueling = false,
                    ExperienceRating = 4,
                    WaterBefore = 500,
                    WaterDuring = 0,
                    LoggingDate = new DateTime(2025, 12, 03),
                    Notes = "Väldigt behagligt"
                },
                new Workout
                {
                    //Id = 3,
                    Fueling = true,
                    ExperienceRating = 5,
                    WaterBefore = 400,
                    WaterDuring = 500,
                    LoggingDate = new DateTime(2025, 12, 06),
                    Notes = "Lätt hela vägen"
                },
                new Workout
                {
                    //Id = 4,
                    Fueling = false,
                    ExperienceRating = 2,
                    WaterBefore = 500,
                    WaterDuring = 0,
                    LoggingDate = new DateTime(2025, 12, 07),
                    Notes = "Extremt tungt men avbröt inte"
                },
                new Workout
                {
                    //Id = 5,
                    Fueling = true,
                    ExperienceRating = 3,
                    WaterBefore = 550,
                    WaterDuring = 0,
                    LoggingDate = new DateTime(2025, 12, 09),
                    Notes = "Testade vingummi, kändes ok"
                },
                new Workout
                {
                    //Id = 6,
                    Fueling = false,
                    ExperienceRating = 3,
                    WaterBefore = 300,
                    WaterDuring = 0,
                    LoggingDate = new DateTime(2025, 12, 11),
                    Notes = "För kort, va stressad och blev inte trött"
                }
            };
            await db.Workouts.AddRangeAsync(testDataWorkouts);
            await db.SaveChangesAsync();

            var testDataExerciseLoggers = new List<ExerciseLogger>
            {
                new ExerciseLogger
                {
                    Duration = new TimeSpan(0, 3, 0),
                    ExerciseId = 1,
                    WorkoutId = 1
                },
                new ExerciseLogger
                {
                    Duration = new TimeSpan(1, 40, 0),
                    ExerciseId = 4,
                    WorkoutId = 1
                },
                new ExerciseLogger
                {
                    Duration = new TimeSpan(0, 2, 0),
                    ExerciseId = 2,
                    WorkoutId = 1
                },
                new ExerciseLogger
                {
                    Duration = new TimeSpan(0, 2, 0),
                    ExerciseId = 1,
                    WorkoutId = 2
                },
                new ExerciseLogger
                {
                    Duration = new TimeSpan(0, 55, 0),
                    ExerciseId = 3,
                    WorkoutId = 2
                },
                new ExerciseLogger
                {
                    Duration = new TimeSpan(0, 5, 0),
                    ExerciseId = 2,
                    WorkoutId = 2
                },
                new ExerciseLogger
                {
                    Duration = new TimeSpan(0, 10, 0),
                    ExerciseId = 1,
                    WorkoutId = 3
                },
                new ExerciseLogger
                {
                    Duration = new TimeSpan(0, 50, 0),
                    ExerciseId = 3,
                    WorkoutId = 3
                },
                new ExerciseLogger
                {
                    Duration = new TimeSpan(0, 20, 0),
                    ExerciseId = 5,
                    WorkoutId = 3
                },
                new ExerciseLogger
                {
                    Duration = new TimeSpan(0, 5, 0),
                    ExerciseId = 1,
                    WorkoutId = 4
                },
                new ExerciseLogger
                {
                    Duration = new TimeSpan(1, 15, 0),
                    ExerciseId = 4,
                    WorkoutId = 4
                },
                new ExerciseLogger
                {
                    Duration = new TimeSpan(0, 1, 30),
                    ExerciseId = 2,
                    WorkoutId = 4
                },
                new ExerciseLogger
                {
                    Duration = new TimeSpan(0, 15, 0),
                    ExerciseId = 1,
                    WorkoutId = 5
                },
                new ExerciseLogger
                {
                    Duration = new TimeSpan(0, 50, 0),
                    ExerciseId = 5,
                    WorkoutId = 5
                },
                new ExerciseLogger
                {
                    Duration = new TimeSpan(0, 10, 0),
                    ExerciseId = 2,
                    WorkoutId = 5
                },
                new ExerciseLogger
                {
                    Duration = new TimeSpan(0, 35, 0),
                    ExerciseId = 5,
                    WorkoutId = 6

                }
            };
            await db.ExerciseLoggers.AddRangeAsync(testDataExerciseLoggers);
            await db.SaveChangesAsync();

        }
    }
}
