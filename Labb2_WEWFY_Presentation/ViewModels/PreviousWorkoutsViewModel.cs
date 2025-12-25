using Labb2_WEWFY_Domain;
using Labb2_WEWFY_Infrastructure.Data.Model;
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
        private ExerciseLogger? _selectedWorkout;
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

        public ExerciseLogger? SelectedWorkout
        {
            get => _selectedWorkout;
            set
            {
                _selectedWorkout = value;
                RaisePropertyChanged();
                //ShowOrderDetailsCommand.RaiseCanExecuteChanged();
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
                                                                .ToListAsync());
        }
    }
}
