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
    }
}


