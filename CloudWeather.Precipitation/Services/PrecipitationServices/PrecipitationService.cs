
using CloudWeather.Precipitation.DataAccess;

namespace CloudWeather.Precipitation.Services.PrecipitationServices;


public class PrecipitationService : IPrecipitationService
{
    private readonly PrecipDbContext _context;

    public PrecipitationService(PrecipDbContext context)
    {
        _context = context;
    }

    public Task GetByZip(string zip, int? days)
    {
        throw new NotImplementedException();
    }
}