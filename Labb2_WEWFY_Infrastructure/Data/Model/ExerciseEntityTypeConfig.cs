using Labb2_WEWFY_Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labb2_WEWFY_Infrastructure.Data.Model;

public class ExerciseEntityTypeConfig : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("Exercises", "dbo");

        builder.Property(e => e.ExerciseName)
               .HasMaxLength(50)
               .IsRequired();

        builder.HasData(
            new Exercise
            {
                Id = 1,
                ExerciseName = "Warm up"
            },
            new Exercise
            {
                Id = 2,
                ExerciseName = "Cool down"
            },
            new Exercise
            {
                Id = 3,
                ExerciseName = "Intervals"
            },
            new Exercise
            {
                Id = 4,
                ExerciseName = "Long run"
            },
            new Exercise
            {
                Id = 5,
                ExerciseName = "Race pace"
            }
        );
    }
}

