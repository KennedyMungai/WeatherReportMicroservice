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
                                                        .Where(precip => precip.ZipCode == zip && precip.CreatedOn > startDate).ToListAsync();

        return await Task.FromResult(results);
    }

    public async Task<bool> RecordPrecipitation(PrecipitationModel model)
    {
        model.CreatedOn = model.CreatedOn.ToUniversalTime();

        try
        {
            await _context.Precipitations.AddAsync(model);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
            throw;
        }
    }
}