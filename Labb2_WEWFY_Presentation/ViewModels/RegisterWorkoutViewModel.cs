using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb2_WEWFY_Domain;
using Labb2_WEWFY_Infrastructure;
using Labb2_WEWFY_Infrastructure.Data.Model;
using Labb2_WEWFY_Presentation.Commands;
using Microsoft.EntityFrameworkCore;

namespace Labb2_WEWFY_Presentation.ViewModels
{
    class RegisterWorkoutViewModel : ViewModelBase
    {
        public ObservableCollection<string>? Exercises { get; set; }
        public DelegateCommand AddNewWorkoutCommand { get; set; }
        private string? _selectedExcersise;

        public string? SelectedExcersise
        {
            get => _selectedExcersise;
            set
            {
                _selectedExcersise = value;

                RaisePropertyChanged("Excersises");
            }
        }
        public RegisterWorkoutViewModel()
        {
            LoadExcersisesAsync();
            AddNewWorkoutCommand = new DelegateCommand(AddNewWorkout); 
        }

        private void AddNewWorkout(object? obj)
        {
            CreateNewWorkoutAsync();

        }

        private async void LoadExcersisesAsync()
        {
            using var db = new WEWFYContext();
            Exercises = new ObservableCollection<string>(await db.Exercises.Select(e => e.ExerciseName).ToListAsync());

            SelectedExcersise = Exercises.FirstOrDefault();
        }
        private async void CreateNewWorkoutAsync()
        {
            using var db = new WEWFYContext();

            var workout = new Workout
            {
                Fueling = true
            };

            db.Add(workout);
            await db.SaveChangesAsync();
        }
    }
}
