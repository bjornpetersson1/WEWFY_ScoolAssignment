using Labb2_WEWFY_Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_WEWFY_Infrastructure.Data.Model;

public class WEWFYContext : DbContext
{
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<ExerciseLogger> ExerciseLoggers { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = new SqlConnectionStringBuilder()
        {
            ServerSPN = "localhost",
            InitialCatalog = "WEWFYDb",
            TrustServerCertificate = true,
            IntegratedSecurity = true
        }.ToString();

        optionsBuilder.UseSqlServer(connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new WorkoutEntityTypeConfig().Configure(modelBuilder.Entity<Workout>());
        new ExerciseLoggerEntityTypeConfig().Configure(modelBuilder.Entity<ExerciseLogger>());
    }
}

