using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreData.Models;

namespace StoreData.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<MessageData>
{
    public void Configure(EntityTypeBuilder<MessageData> builder)
    {
        builder
            .HasKey(m => m.Id);
        builder
            .HasOne(m => m.User)
            .WithMany(u => u.Messages)
            .IsRequired();
    }
}