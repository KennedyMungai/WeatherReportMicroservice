using CloudWeather.Report.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace CloudWeather.Report.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherReportController : ControllerBase
{
    private readonly IWeatherReportAggregator _weatherReportAggregator;

    public WeatherReportController(IWeatherReportAggregator weatherReportAggregator)
    {
        _weatherReportAggregator = weatherReportAggregator;
    }

    [HttpGet("/weather-report/{zip}")]
    public async Task<IActionResult> GetWeatherReport(string zip, [FromQuery] int? days)
    {
        if (days == null || days < 1 || days > 30)
        {
            return BadRequest("Days parameter is required");
        }

        var weatherData = await _weatherReportAggregator.BuildWeeklyReport(zip, days);

        return await Task.FromResult(Ok(weatherData));
    }
}