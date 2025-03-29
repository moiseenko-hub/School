using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreData.Models;

namespace StoreData.Configurations;

public class LessonConfiguration : IEntityTypeConfiguration<LessonData>
{
    public void Configure(EntityTypeBuilder<LessonData> builder)
    {
        builder.HasKey(l => l.Id);
        builder
            .HasMany(c => c.Comments)
            .WithOne(l => l.Lesson)
            .HasForeignKey(l => l.LessonId)
            .IsRequired();
    }
}