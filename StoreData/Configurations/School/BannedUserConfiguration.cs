using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreData.Models;

namespace StoreData.Configurations;

public class BannedUserConfiguration : IEntityTypeConfiguration<BannedUserData>
{
    public void Configure(EntityTypeBuilder<BannedUserData> builder)
    {
        builder
            .HasKey(bu => bu.Id);
    }
}