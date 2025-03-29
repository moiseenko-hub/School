using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreData.Models;

namespace StoreData.Configurations;

public class LessonCommentConfiguration : IEntityTypeConfiguration<LessonCommentData>
{
    public void Configure(EntityTypeBuilder<LessonCommentData> builder)
    {
        builder.HasKey(c => c.Id);
        builder
            .HasOne(c => c.Lesson)
            .WithMany(l => l.Comments)
            .HasForeignKey(c => c.LessonId)
            .IsRequired();
        builder
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .IsRequired();
    }
}