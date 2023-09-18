
using CloudWeather.Precipitation.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CloudWeather.Precipitation.Services.PrecipitationServices;


public class PrecipitationService : IPrecipitationService
{
    private readonly PrecipDbContext _context;

    public PrecipitationService(PrecipDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PrecipitationModel>> GetByZip(string zip, int? days)
    {
        var startDate = DateTime.UtcNow - TimeSpan.FromDays(days!.Value);
        List<PrecipitationModel> results = await _context.Precipitations
                                                        .Where(precip => precip.ZipCode == zip).ToListAsync();

        return await Task.FromResult(results);
    }
}