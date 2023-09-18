using CloudWeather.Precipitation.DataAccess;

namespace CloudWeather.Precipitation.Services.PrecipitationServices;


public interface IPrecipitationService
{
    Task<IEnumerable<PrecipitationModel>> GetByZip(string zip, int? days);
}