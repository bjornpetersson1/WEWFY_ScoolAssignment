using Labb2_WEWFY_Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labb2_WEWFY_Infrastructure.Data.Model;

public class ExerciseLoggerEntityTypeConfig : IEntityTypeConfiguration<ExerciseLogger>
{
    public void Configure(EntityTypeBuilder<ExerciseLogger> builder)
    {
        builder.HasKey(w => w.Id);

        builder.ToTable("ExerciseLoggers", "dbo");

        builder.HasData(
            new ExerciseLogger
            {
                Id = 1,
                Duration = new TimeSpan(0, 3, 0),
                ExerciseId = 1,
                WorkoutId = 1
            },
            new ExerciseLogger
            {
                Id = 2,
                Duration = new TimeSpan(1, 40, 0),
                ExerciseId = 4,
                WorkoutId = 1
            },
            new ExerciseLogger
            {
                Id = 3,
                Duration = new TimeSpan(0, 2, 0),
                ExerciseId = 2,
                WorkoutId = 1
            },
            new ExerciseLogger
            {
                Id = 4,
                Duration = new TimeSpan(0, 2, 0),
                ExerciseId = 1,
                WorkoutId = 2
            },
            new ExerciseLogger
            {
                Id = 5,
                Duration = new TimeSpan(0, 55, 0),
                ExerciseId = 3,
                WorkoutId = 2
            },
            new ExerciseLogger
            {
                Id = 6,
                Duration = new TimeSpan(0, 5, 0),
                ExerciseId = 2,
                WorkoutId = 2
            },
            new ExerciseLogger
            {
                Id = 7,
                Duration = new TimeSpan(0, 10, 0),
                ExerciseId = 1,
                WorkoutId = 3
            },
            new ExerciseLogger
            {
                Id = 8,
                Duration = new TimeSpan(0, 50, 0),
                ExerciseId = 3,
                WorkoutId = 3
            },
            new ExerciseLogger
            {
                Id = 9,
                Duration = new TimeSpan(0, 20, 0),
                ExerciseId = 5,
                WorkoutId = 3
            },
            new ExerciseLogger
            {
                Id = 10,
                Duration = new TimeSpan(0, 5, 0),
                ExerciseId = 1,
                WorkoutId = 4
            },
            new ExerciseLogger
            {
                Id = 11,
                Duration = new TimeSpan(1, 15, 0),
                ExerciseId = 4,
                WorkoutId = 4
            },
            new ExerciseLogger
            {
                Id = 12,
                Duration = new TimeSpan(0, 1, 30),
                ExerciseId = 2,
                WorkoutId = 4
            },
            new ExerciseLogger
            {
                Id = 13,
                Duration = new TimeSpan(0, 15, 0),
                ExerciseId = 1,
                WorkoutId = 5
            },
            new ExerciseLogger
            {
                Id = 14,
                Duration = new TimeSpan(0, 50, 0),
                ExerciseId = 5,
                WorkoutId = 5
            },
            new ExerciseLogger
            {
                Id = 15,
                Duration = new TimeSpan(0, 10, 0),
                ExerciseId = 2,
                WorkoutId = 5
            },
            new ExerciseLogger
            {
                Id = 16,
                Duration = new TimeSpan(0, 35, 0),
                ExerciseId = 5,
                WorkoutId = 6

            }
            );
    }
}


