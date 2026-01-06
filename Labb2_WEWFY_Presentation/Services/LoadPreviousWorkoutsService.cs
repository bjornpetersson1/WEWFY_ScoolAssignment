using Labb2_WEWFY_Domain;
using Labb2_WEWFY_Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_WEWFY_Presentation.Services
{
    public class LoadPreviousWorkoutsService
    {
        public async Task<List<Workout>> GetPreviousWorkoutsAsync()
        {
            using var db = new WEWFYContext();

            return await db.Workouts
                           .Include(w => w.ExerciseLoggers)
                           .ThenInclude(el => el.Exercise)
                           .ToListAsync();
        }
    }
}
