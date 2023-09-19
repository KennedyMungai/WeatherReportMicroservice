using CloudWeather.Temperature.DataAccess;

namespace CloudWeather.Temperature.Services.TemperatureService;


public interface ITemperatureService
{
    Task<IEnumerable<TemperatureModel>> GetTemperatureData(string zip, int? days);
}