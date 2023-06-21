using BeItmoBackend.Data.Categories.Models;
using BeItmoBackend.Data.Happiness.Models;
using BeItmoBackend.Data.Interests.Models;
using BeItmoBackend.Data.UniversityEvents.Models;
using BeItmoBackend.Data.Users.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BeItmoBackend.Data;

public class BeItmoContext : DbContext
{
    public BeItmoContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<CategoryDbModel> Categories { get; set; } = null!;
    public DbSet<HappinessCheckpointDbModel> HappinessCheckpoints { get; set; } = null!;
    public DbSet<UniversityEventDbModel> UniversityEvents { get; set; } = null!;
    public DbSet<InterestDbModel> Interests { get; set; } = null!;
    public DbSet<InterestStatisticDbModel> InterestStatistics { get; set; } = null!;
    public DbSet<UserDbModel> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BeItmoContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSnakeCaseNamingConvention()
            .UseLazyLoadingProxies();
    }

    public class Factory : IDesignTimeDbContextFactory<BeItmoContext>
    {
        public BeItmoContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder()
                .UseNpgsql("FakeConnectionStringOnlyForMigrations")
                .Options;

            return new BeItmoContext(options);
        }
    }
}