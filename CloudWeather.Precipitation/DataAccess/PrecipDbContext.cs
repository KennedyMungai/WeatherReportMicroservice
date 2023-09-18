using Microsoft.EntityFrameworkCore;

namespace CloudWeather.Precipitation.DataAccess;

public class PrecipDbContext : DbContext
{
    public PrecipDbContext(DbContextOptions<PrecipDbContext> options) : base(options)
    {

    }
}