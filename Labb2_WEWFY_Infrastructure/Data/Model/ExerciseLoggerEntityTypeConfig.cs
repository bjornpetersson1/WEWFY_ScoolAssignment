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
//public class Blog //Entitetsklass
//{
//    public int Id { get; set; }
//    public string? Url { get; set; }
//    public int? RatingRenamed { get; set; } // ? gör att värdet kan va null i databasen
//    public List<Post> Posts { get; set; } // det här skapar en foreign key i Posts som är kopplat till blogs (one to many-koppling)
//}

//public class Post
//{
//    public int Id { get; set; }
//    public string? Caption { get; set; }
//    public string? Body { get; set; }
//    public string? Author { get; set; }
//    public DateTime CreatedAt { get; set; }

//    //Dom här två nedan behövs inte för att skapa one-to-many kopplingen men är bra att ha om man vill komma åt t ex id:t eller vilken blog som posten är kopplad till
//    public int BlogId { get; set; }
//    public Blog Blog { get; set; }
//}

