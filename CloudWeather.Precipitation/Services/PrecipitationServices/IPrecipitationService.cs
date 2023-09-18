namespace CloudWeather.Precipitation.Services.PrecipitationServices;


public interface IPrecipitationService
{
    Task GetByZip(string zip, int? days);
}