using Microsoft.EntityFrameworkCore;
using StoreData.Configurations;
using StoreData.Models;

namespace StoreData;

public class SchoolDbContext : DbContext
{
    public const string CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SchoolDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    public DbSet<LessonData> Lessons { get; set; }
    public DbSet<LessonCommentData> Comments { get; set; }
    public DbSet<SchoolUserData> Users { get; set; }
    public DbSet<SchoolRoleData> Roles { get; set; }
    public DbSet<BannedUserData> BannedUsers { get; set; }
    public DbSet<BanWordData> BanWords { get; set; }
    public DbSet<MessageData> Messages { get; set; }
    public SchoolDbContext(){}
    public SchoolDbContext(DbContextOptions<SchoolDbContext> option) : base(option) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(CONNECTION_STRING);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new LessonConfiguration());
        modelBuilder.ApplyConfiguration(new LessonCommentConfiguration());
        modelBuilder.ApplyConfiguration(new SchoolUserConfiguration());
        modelBuilder.ApplyConfiguration(new SchoolRoleConfiguration());
        modelBuilder.ApplyConfiguration(new BannedUserConfiguration());
        modelBuilder.ApplyConfiguration(new BanWordConfiguration());
        modelBuilder.ApplyConfiguration(new MessageConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}