using System;
namespace CloudWeather.DataLoader.Models;


public class TemperatureModel
{
    public DateTime CreatedOn { get; set; }
    public decimal AmountInches { get; set; }
    public string WeatherType { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
}