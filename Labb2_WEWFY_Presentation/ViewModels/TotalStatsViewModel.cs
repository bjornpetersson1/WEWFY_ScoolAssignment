using Labb2_WEWFY_Domain;
using Labb2_WEWFY_Infrastructure.Data.Model;
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
    class TotalStatsViewModel : ViewModelBase
    {
        private bool _filterWarmUp;
        public bool FilterWarmUp
        {
            get => _filterWarmUp;
            set
            {
                _filterWarmUp = value;
                RaisePropertyChanged();
                ApplyExerciseFilter();
            }
        }

        private bool _filterCoolDown;
        public bool FilterCoolDown
        {
            get => _filterCoolDown;
            set
            {
                _filterCoolDown = value;
                RaisePropertyChanged();
                ApplyExerciseFilter();
            }
        }

        private bool _filterIntervals;
        public bool FilterIntervals
        {
            get => _filterIntervals;
            set
            {
                _filterIntervals = value;
                RaisePropertyChanged();
                ApplyExerciseFilter();
            }
        }
        
        private bool _filterLongRun;
        public bool FilterLongRun
        {
            get => _filterLongRun;
            set
            {
                _filterLongRun = value;
                RaisePropertyChanged();
                ApplyExerciseFilter();
            }
        }

        private bool _filterRacePace;
        public bool FilterRacePace
        {
            get => _filterRacePace;
            set
            {
                _filterRacePace = value;
                RaisePropertyChanged();
                ApplyExerciseFilter();
            }
        }

        private bool _filterFueling;
        public bool FilterFueling
        {
            get => _filterFueling;
            set
            {
                _filterFueling = value;
                RaisePropertyChanged();
                ApplyExerciseFilter();
            }
        }

        private bool _filterWaterBefore;
        public bool FilterWaterBefore
        {
            get => _filterWaterBefore;
            set
            {
                _filterWaterBefore = value;
                RaisePropertyChanged();
                ApplyExerciseFilter();
            }
        }

        private bool _filterWaterDuring;
        public bool FilterWaterDuring
        {
            get => _filterWaterDuring;
            set
            {
                _filterWaterDuring = value;
                RaisePropertyChanged();
                ApplyExerciseFilter();
            }
        }

        private bool _filterRatedOne;
        public bool FilterRatedOne
        {
            get => _filterRatedOne;
            set
            {
                _filterRatedOne = value;
                RaisePropertyChanged();
                ApplyExerciseFilter();
            }
        }

        private bool _filterRatedTwo;
        public bool FilterRatedTwo
        {
            get => _filterRatedTwo;
            set
            {
                _filterRatedTwo = value;
                RaisePropertyChanged();
                ApplyExerciseFilter();
            }
        }

        private bool _filterRatedThree;
        public bool FilterRatedThree
        {
            get => _filterRatedThree;
            set
            {
                _filterRatedThree = value;
                RaisePropertyChanged();
                ApplyExerciseFilter();
            }
        }

        private bool _filterRatedFour;
        public bool FilterRatedFour
        {
            get => _filterRatedFour;
            set
            {
                _filterRatedFour = value;
                RaisePropertyChanged();
                ApplyExerciseFilter();
            }
        }

        private bool _filterRatedFive;
        public bool FilterRatedFive
        {
            get => _filterRatedFive;
            set
            {
                _filterRatedFive = value;
                RaisePropertyChanged();
                ApplyExerciseFilter();
            }
        }

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
        private ObservableCollection<WorkoutExerciseRow> allWorkoutRows;
        private ObservableCollection<WorkoutExerciseRow> filteredWorkoutRows;
        public ObservableCollection<WorkoutExerciseRow> FilteredWorkoutRows 
        {
            get => filteredWorkoutRows;
            set
            {
                filteredWorkoutRows = value;
                RaisePropertyChanged();
            }
        }

        public TotalStatsViewModel()
        {

        }

        public async Task LoadPreviousWorkoutsAsync()
        {
            using var db = new WEWFYContext();
            var workouts = await db.Workouts
                                    .Include(w => w.ExerciseLoggers)
                                    .ThenInclude(el => el.Exercise)
                                    .AsNoTracking()
                                    .ToListAsync();
            var rows = workouts
                    .SelectMany(w => w.ExerciseLoggers.Select(el => new WorkoutExerciseRow
                    {
                        WorkoutId = w.Id,
                        Date = w.LoggingDate,
                        WaterBefore = w.WaterBefore,
                        WaterDuring = w.WaterDuring,
                        Fueling = w.Fueling,
                        Rating = w.ExperienceRating,
                        TotalDuration = TimeSpan.FromTicks(w.ExerciseLoggers.Sum(e => e.Duration.Ticks)),
                        NumOfExercises = w.ExerciseLoggers.Count,
                        Duration = el.Duration,
                        ExerciseName = el.Exercise.ExerciseName
                    }))
                    .ToList();
            allWorkoutRows = new ObservableCollection<WorkoutExerciseRow>(rows);
            FilteredWorkoutRows = new ObservableCollection<WorkoutExerciseRow>();
        }
        private void ApplyExerciseFilter()
        {
            if (allWorkoutRows == null) return;

            var selectedExercises = new List<string>();
            if (FilterWarmUp) selectedExercises.Add("Warm up");
            if (FilterCoolDown) selectedExercises.Add("Cool down");
            if (FilterIntervals) selectedExercises.Add("Intervals");
            if (FilterLongRun) selectedExercises.Add("Long run");
            if (FilterRacePace) selectedExercises.Add("Race pace");

            var selectedRatings = new List<int>();
            if (FilterRatedOne) selectedRatings.Add(1);
            if (FilterRatedTwo) selectedRatings.Add(2);
            if (FilterRatedThree) selectedRatings.Add(3);
            if (FilterRatedFour) selectedRatings.Add(4);
            if (FilterRatedFive) selectedRatings.Add(5);

            bool anyFilterActive =
                    selectedExercises.Count > 0 ||
                    selectedRatings.Count > 0 ||
                    FilterFueling ||
                    FilterWaterBefore ||
                    FilterWaterDuring;

            if (!anyFilterActive)
            {
                FilteredWorkoutRows = new ObservableCollection<WorkoutExerciseRow>();
                return;
            }

            var filtered = allWorkoutRows.Where(row =>
                (selectedExercises.Count == 0 || selectedExercises.Contains(row.ExerciseName)) &&
                (selectedRatings.Count == 0 || selectedRatings.Contains(row.Rating)) &&
                (!FilterFueling || row.Fueling == true) &&
                (!FilterWaterBefore || row.WaterBefore > 0) &&
                (!FilterWaterDuring || row.WaterDuring > 0) 
            );

            FilteredWorkoutRows = new ObservableCollection<WorkoutExerciseRow>(filtered);
        }

    }
}
