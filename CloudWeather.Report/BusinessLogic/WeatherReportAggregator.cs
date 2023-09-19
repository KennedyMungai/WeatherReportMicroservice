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
    Task<WeatherReport> BuildWeeklyReport(string zip, int days);
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

    public async Task<WeatherReport> BuildWeeklyReport(string zip, int days)
    {
        var httpClient = _http.CreateClient();

        var precipData = await FetchPrecipitationData(httpClient, zip, days);
        var tempData = await FetchTemperatureData(httpClient, zip, days);
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

    private string? BuildTemperatureServiceEndpoint(string zip, int days)
    {
        var tempServiceProtocol = _config.TempDataProtocol;
        var tempServiceHost = _config.TempDataHost;
        var tempServicePort = _config.TempDataPort;

        return $"{tempServiceProtocol}://{tempServiceHost}:{tempServicePort}/observation/{zip}?days={days}";
    }

    private async Task<List<PrecipitationModel>> FetchPrecipitationData(HttpClient httpClient, string zip, int days)
    {
        throw new NotImplementedException();
    }
}