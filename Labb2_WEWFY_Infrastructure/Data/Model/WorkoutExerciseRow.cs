using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_WEWFY_Infrastructure.Data.Model
{
    public class WorkoutExerciseRow
    {
        public int WorkoutId { get; set; }
        public DateTime Date { get; set; }
        public int? WaterBefore { get; set; }
        public int? WaterDuring { get; set; }
        public required bool Fueling { get; set; }
        public int Rating { get; set; }
        public TimeSpan TotalDuration { get; set; }
        public int NumOfExercises { get; set; }
        public TimeSpan Duration { get; set; }
        public string ExerciseName { get; set; }

    }

}
