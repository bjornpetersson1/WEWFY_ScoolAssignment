using Labb2_WEWFY_Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labb2_WEWFY_Infrastructure.Data.Model;

public class WorkoutEntityTypeConfig : IEntityTypeConfiguration<Workout>
{
    public void Configure(EntityTypeBuilder<Workout> builder)
    {
        builder.HasKey(w => w.Id);

        builder.ToTable("Workouts", "dbo", table =>
        {
            table.HasCheckConstraint(
                "CK_Workouts_ExperienceRating",
                "[ExperienceRating] >= 0 AND [ExperienceRating] <= 5");
            table.HasCheckConstraint(
                "CK_Workouts_WaterDuring",
                "[ExperienceRating] >= 0 AND [ExperienceRating] <= 3000");
            table.HasCheckConstraint(
                "CK_Workouts_WaterBefore",
                "[ExperienceRating] >= 0 AND [ExperienceRating] <= 3000");
        });

        builder.Property(w => w.Notes)
               .HasMaxLength(500);

        builder.Property(w => w.ExperienceRating)
               .IsRequired();

        builder.Property(w => w.Fueling)
               .IsRequired();

        builder.HasData(
            //new Workout
            //{
            //    Id = 1,
            //    Fueling = true,
            //    ExperienceRating = 3,
            //    WaterBefore = 330,
            //    WaterDuring = 0,
            //    LoggingDate = new DateTime(2025, 12, 01),
            //    Notes = "Spydde, annars bra"
            //},
            //new Workout
            //{
            //    Id = 2,
            //    Fueling = false,
            //    ExperienceRating = 4,
            //    WaterBefore = 500,
            //    WaterDuring = 0,
            //    LoggingDate = new DateTime(2025, 12, 03),
            //    Notes = "Väldigt behagligt"
            //},
            //new Workout
            //{
            //    Id = 3,
            //    Fueling = true,
            //    ExperienceRating = 5,
            //    WaterBefore = 400,
            //    WaterDuring = 500,
            //    LoggingDate = new DateTime(2025, 12, 06),
            //    Notes = "Lätt hela vägen"
            //},
            //new Workout
            //{
            //    Id = 4,
            //    Fueling = false,
            //    ExperienceRating = 2,
            //    WaterBefore = 500,
            //    WaterDuring = 0,
            //    LoggingDate = new DateTime(2025, 12, 07),
            //    Notes = "Extremt tungt men avbröt inte"
            //},
            //new Workout
            //{
            //    Id = 5,
            //    Fueling = true,
            //    ExperienceRating = 3,
            //    WaterBefore = 550,
            //    WaterDuring = 0,
            //    LoggingDate = new DateTime(2025, 12, 09),
            //    Notes = "Testade vingummi, kändes ok"
            //},
            //new Workout
            //{
            //    Id = 6,
            //    Fueling = false,
            //    ExperienceRating = 3,
            //    WaterBefore = 300,
            //    WaterDuring = 0,
            //    LoggingDate = new DateTime(2025, 12, 11),
            //    Notes = "För kort, va stressad och blev inte trött"
            //}
        );
    }
}


