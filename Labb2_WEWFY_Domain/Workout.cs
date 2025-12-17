using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_WEWFY_Domain
{
    public class Workout
    {
        public int Id { get; set; }
        public DateTime LoggingDate { get; set; }
        public int? WaterBefore { get; set; }
        public int? WaterDuring { get; set; }
        public required bool Fueling { get; set; }
        public string? Notes { get; set; }
        public int ExperienceRating { get; set; }
        public List<ExerciseLogger> ExerciseLoggers { get; set; }
    }
}
