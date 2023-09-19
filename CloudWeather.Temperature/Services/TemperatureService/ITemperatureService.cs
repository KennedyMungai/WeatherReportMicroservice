namespace CloudWeather.Temperature.Services.TemperatureService;


public interface ITemperatureService
{
    Task GetTemperatureData(string zip, int? days);
}