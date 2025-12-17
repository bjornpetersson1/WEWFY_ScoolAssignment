using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_WEWFY_Domain
{
    public class Exercise
    {
        public int Id { get; set; }
        public required string ExerciseName { get; set; }
        public List<ExerciseLogger> ExerciseLoggers { get; set; }
    }
}
