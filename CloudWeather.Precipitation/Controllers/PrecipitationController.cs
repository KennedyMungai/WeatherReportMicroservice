using CloudWeather.Precipitation.DataAccess;
using CloudWeather.Precipitation.Services.PrecipitationServices;
using Microsoft.AspNetCore.Mvc;

namespace CloudWeather.Precipitation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrecipitationController : ControllerBase
{
    private readonly IPrecipitationService _precipService;

    public PrecipitationController(IPrecipitationService precipService)
    {
        _precipService = precipService;
    }

    [HttpGet("/observation/{zip}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByZip([FromBody] string zip, [FromQuery] int? days)
    {
        if (days is null || days > 30 || days < 1)
        {
            return await Task.FromResult(BadRequest("Please enter the value for days and it should be less than 30"));
        }

        if (zip is null)
        {
            return await Task.FromResult(BadRequest("The zip code has not been entered"));
        }

        var results = _precipService.GetByZip(zip, days);

        return await Task.FromResult(Ok(results));
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RecordPrecipitation([FromBody] PrecipitationModel precip)
    {
        var result = await _precipService.RecordPrecipitation(precip);

        if (result is false)
        {
            return BadRequest("Something went wrong");
        }

        return await Task.FromResult(Ok());
    }
}