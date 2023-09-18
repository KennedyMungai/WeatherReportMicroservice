using Microsoft.EntityFrameworkCore;

namespace CloudWeather.Temperature.DataAccess;


public class TempDbContext : DbContext
{
    public TempDbContext(DbContextOptions<TempDbContext> options) : base(options)
    {

    }

    public DbSet<TemperatureModel> TemperatureTable { get; set; }
}