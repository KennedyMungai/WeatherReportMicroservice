using CloudWeather.Report.DataAccess;
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

    public Task<WeatherReport> BuildWeeklyReport(string zip, int days)
    {
        throw new NotImplementedException();
    }
}