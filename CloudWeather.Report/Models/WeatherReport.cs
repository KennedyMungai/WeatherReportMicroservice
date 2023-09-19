namespace CloudWeather.Report.Models;


public class WeatherReport
{
    public decimal AverageHighF { get; set; }
    public decimal AverageLowF { get; set; }
    public decimal RainfallTotalInches { get; set; }
    public decimal SnowTotalInches { get; set; }
    public string ZipCode { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
}