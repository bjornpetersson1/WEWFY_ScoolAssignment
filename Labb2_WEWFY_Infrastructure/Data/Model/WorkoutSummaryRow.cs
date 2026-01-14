using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_WEWFY_Infrastructure.Data.Model
{
    public class WorkoutSummaryRow
    {
        public int WorkoutId { get; set; }
        public DateTime Date { get; set; }
        public string Tempos { get; set; } = "";
        public TimeSpan TotalDuration { get; set; }
        public int? WaterBefore { get; set; }
        public int? WaterDuring { get; set; }
        public bool Fueling { get; set; }
        public int Rating { get; set; }
        public string? Notes { get; set; }
    }

}
