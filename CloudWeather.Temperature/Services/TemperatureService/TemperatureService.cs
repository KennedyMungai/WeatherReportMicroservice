
using CloudWeather.Temperature.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CloudWeather.Temperature.Services.TemperatureService;


public class TemperatureService : ITemperatureService
{
    private readonly TempDbContext _context;

    public TemperatureService(TempDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TemperatureModel>?> GetTemperatureData(string zip, int? days)
    {
        try
        {
            var startDate = DateTime.UtcNow - TimeSpan.FromDays(days.Value);
            var results = await _context.TemperatureTable
                                    .Where(temp => temp.ZipCode == zip && temp.CreatedOn > startDate)
                                    .ToListAsync();
            return results;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}