using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreData.Models;

namespace StoreData.Configurations;

public class SchoolUserConfiguration : IEntityTypeConfiguration<SchoolUserData>
{
    public void Configure(EntityTypeBuilder<SchoolUserData> builder)
    {
        builder.HasKey(u => u.Id);
        builder
            .HasMany(u => u.Comments)
            .WithOne(c => c.User)
            .IsRequired();
        builder
            .HasOne(u => u.Role)
            .WithMany(r => r.Users);
    }
}