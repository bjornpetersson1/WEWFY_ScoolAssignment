using Labb2_WEWFY_Domain;
using Labb2_WEWFY_Presentation.Services;
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
        private readonly LoadPreviousWorkoutsService _LoadPreviousWorkoutsService;
        public TotalStatsViewModel()
        {
            _LoadPreviousWorkoutsService = new LoadPreviousWorkoutsService();
        }

        public async Task LoadPreviousWorkoutsAsync()
        {
            var workouts = await _LoadPreviousWorkoutsService.GetPreviousWorkoutsAsync();
            Workouts = new ObservableCollection<Workout>(workouts);
        }

    }
}
