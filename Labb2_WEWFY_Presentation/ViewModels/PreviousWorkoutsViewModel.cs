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
        public ObservableCollection<ExerciseLogger> Workouts { get; set; }
        private ExerciseLogger? _selectedWorkout;
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
            Workouts = new ObservableCollection<ExerciseLogger>(await db.ExerciseLoggers.ToListAsync());
        }
    }
}
