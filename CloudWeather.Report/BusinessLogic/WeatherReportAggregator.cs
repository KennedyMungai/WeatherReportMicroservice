using System.Text.Json;
using CloudWeather.Report.Config;
using CloudWeather.Report.DataAccess;
using CloudWeather.Report.Models;
using Microsoft.Extensions.Options;

namespace CloudWeather.Report.BusinessLogic;

public interface IWeatherReportAggregator
{
    /// <summary>
    ///  Takes a bunch of reports from different sources and builds a weekly report.
    /// </summary>
    /// <param name="zip">The zip code for the area for which the weather report is needed</param>
    /// <param name="days">Th number of days for which the weather report is needed</param>
    /// <returns>A weather report</returns>
    Task<ReportsModel> BuildWeeklyReport(string zip, int days);
}

public class WeatherReportAggregator : IWeatherReportAggregator
{
    private readonly IHttpClientFactory _http;
    private readonly ILogger<WeatherReportAggregator> _logger;
    private readonly WeatherDataConfig _config;
    private readonly ReportDbContext _context;

    public WeatherReportAggregator(
        ReportDbContext context,
        IHttpClientFactory http,
        ILogger<WeatherReportAggregator> logger,
        IOptions<WeatherDataConfig> config
    )
    {
        _context = context;
        _http = http;
        _logger = logger;
        _config = config.Value;
    }

    public async Task<ReportsModel> BuildWeeklyReport(string zip, int days)
    {
        var httpClient = _http.CreateClient();

        var precipData = await FetchPrecipitationData(httpClient, zip, days);
        var totalSnow = GetTotalSnow(precipData);
        var totalRain = GetTotalRain(precipData);
        _logger.LogInformation(
            $"zip: {zip} over last {days} days:" +
            $"total snow: {totalSnow} inches, rain: {totalRain} inches"
        );

        var tempData = await FetchTemperatureData(httpClient, zip, days);
        var averageHighTemp = tempData.Average(t => t.TempHighF);
        var averageLowTemp = tempData.Average(t => t.TempLowF);
        _logger.LogInformation(
            $"zip: {zip} over the last {days} days:" +
            $"average high temp: {averageHighTemp} degrees, average low temp: {averageLowTemp} degrees"
        );

        var weatherReport = new ReportsModel
        {
            AverageHighF = Math.Round(averageHighTemp, 1),
            AverageLowF = Math.Round(averageLowTemp, 1),
            RainfallTotalInches = totalRain,
            SnowTotalInches = totalSnow,
            ZipCode = zip,
            CreatedOn = DateTime.UtcNow
        };

        // TODO: Add some caching for the weather reports to save on db read operations
        await _context.Reports.AddAsync(weatherReport);
        await _context.SaveChangesAsync();

        return weatherReport;
    }

    private static decimal GetTotalRain(List<PrecipitationModel> precipData)
    {
        var totalRain = precipData
                            .Where(p => p.WeatherType == "rain")
                            .Sum(p => p.AmountInches);
        return Math.Round(totalRain, 1);
    }

    private static decimal GetTotalSnow(List<PrecipitationModel> precipData)
    {
        var totalSnow = precipData
                            .Where(p => p.WeatherType == "snow")
                            .Sum(p => p.AmountInches);
        return Math.Round(totalSnow, 1);
    }

    private async Task<List<TemperatureModel>> FetchTemperatureData(HttpClient httpClient, string zip, int days)
    {

        var endpoint = BuildTemperatureServiceEndpoint(zip, days);
        var temperatureRecords = await httpClient.GetAsync(endpoint);
        var temperatureData = await temperatureRecords
                                    .Content
                                    .ReadFromJsonAsync<List<TemperatureModel>>();

        return temperatureData ?? new List<TemperatureModel>();
    }

    private string BuildTemperatureServiceEndpoint(string zip, int days)
    {
        var tempServiceProtocol = _config.TempDataProtocol;
        var tempServiceHost = _config.TempDataHost;
        var tempServicePort = _config.TempDataPort;

        return $"{tempServiceProtocol}://{tempServiceHost}:{tempServicePort}/observation/{zip}?days={days}";
    }

    private string BuildPrecipitationServiceEndpoint(string zip, int days)
    {
        var precipServiceProtocol = _config.PrecipDataProtocol;
        var precipServiceHost = _config.PrecipDataHost;
        var precipServicePort = _config.PrecipDataPort;

        return $"{precipServiceProtocol}://{precipServiceHost}:{precipServicePort}/observation/{zip}?days={days}";
    }

    private async Task<List<PrecipitationModel>> FetchPrecipitationData(HttpClient httpClient, string zip, int days)
    {
        var endpoint = BuildPrecipitationServiceEndpoint(zip, days);
        var precipRecords = await httpClient.GetAsync(endpoint);
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var precipData = await precipRecords
                                .Content
                                .ReadFromJsonAsync<List<PrecipitationModel>>(jsonSerializerOptions);

        return precipData ?? new List<PrecipitationModel>();
    }
}