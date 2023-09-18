using Microsoft.EntityFrameworkCore;

namespace CloudWeather.Precipitation.DataAccess;

public class PrecipDbContext : DbContext
{
    public PrecipDbContext(DbContextOptions<PrecipDbContext> options) : base(options)
    {

    }

    public DbSet<PrecipitationModel> Precipitations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        SnakeCaseIdentityTableNames(modelBuilder);
    }

    public static void SnakeCaseIdentityTableNames(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PrecipitationModel>(b =>
        {
            b.ToTable("precipitation");
        });
    }
}