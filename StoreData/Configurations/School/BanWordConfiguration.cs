using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreData.Models;

namespace StoreData.Configurations;

public class BanWordConfiguration : IEntityTypeConfiguration<BanWordData>
{
    public void Configure(EntityTypeBuilder<BanWordData> builder)
    {
        builder
            .HasKey(bw => bw.Id);
    }
}