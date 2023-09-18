using Microsoft.EntityFrameworkCore;

namespace CloudWeather.Report.DataAccess;


public class ReportDbContext : DbContext
{
    public ReportDbContext(DbContextOptions<ReportDbContext> options) : base(options)
    {

    }

    public DbSet<ReportsModel> Reports { get; set; }
}