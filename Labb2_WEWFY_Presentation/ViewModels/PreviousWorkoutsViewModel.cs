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
            }
        }
        public PreviousWorkoutsViewModel()
        {
            LoadPreviousWorkoutsAsync();
        }

        private async void LoadPreviousWorkoutsAsync()
        {
            using var db = new WEWFYContext();
            Workouts = new ObservableCollection<Workout>(await db.Workouts
                                                                .Include(w => w.ExerciseLoggers)
                                                                .ThenInclude(el => el.Exercise)
                                                                .ToListAsync());
        }
    }
}
