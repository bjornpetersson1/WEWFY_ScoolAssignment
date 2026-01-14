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
                "[WaterDuring] >= 0 AND [WaterDuring] <= 3000");
            table.HasCheckConstraint(
                "CK_Workouts_WaterBefore",
                "[WaterBefore] >= 0 AND [WaterBefore] <= 3000");
        });

        builder.Property(w => w.Notes)
               .HasMaxLength(500);

        builder.Property(w => w.ExperienceRating)
               .IsRequired();

        builder.Property(w => w.Fueling)
               .IsRequired();
    }
}


