using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb2_WEWFY_Infrastructure;
using Labb2_WEWFY_Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Labb2_WEWFY_Presentation.ViewModels
{
    class RegisterWorkoutViewModel : ViewModelBase
    {
        public ObservableCollection<string>? Exercises { get; set; }
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
        }
        private async void LoadExcersisesAsync()
        {
            using var db = new WEWFYContext();
            Exercises = new ObservableCollection<string>(await db.Exercises.Select(e => e.ExerciseName).ToListAsync());

            SelectedExcersise = Exercises.FirstOrDefault();
        }
    }
}
