using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_WEWFY_Presentation.ViewModels
{
    public class ExerciseLoggerViewModel
    {
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
