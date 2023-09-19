using CloudWeather.Temperature.Services.TemperatureService;
using Microsoft.AspNetCore.Mvc;

namespace CloudWeather.Temperature.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TemperatureController : ControllerBase
{
    private readonly ITemperatureService _temperatureService;

    public TemperatureController(ITemperatureService temperatureService)
    {
        _temperatureService = temperatureService;
    }

    [HttpGet("/observation/{zipCode}")]
    public async Task<IActionResult> GetTemperatureByZipCode(string zipCode, int? days)
    {
        if (days is null || days < 1 || days > 30)
        {
            return await Task.FromResult(BadRequest("Days must be between 1 and 30"));
        }

        var results = await _temperatureService.GetTemperatureData(zipCode, (int)days);

        if (results is null)
        {
            return await Task.FromResult(NotFound("No temperature data found"));
        }

        return await Task.FromResult(Ok(results));
    }
}