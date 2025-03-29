using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreData.Models;

namespace StoreData.Configurations;

public class SchoolRoleConfiguration : IEntityTypeConfiguration<SchoolRoleData>
{
    public void Configure(EntityTypeBuilder<SchoolRoleData> builder)
    {
        builder.HasKey(r => r.Id);
        builder
            .HasMany(u => u.Users)
            .WithOne(r => r.Role);
    }
}