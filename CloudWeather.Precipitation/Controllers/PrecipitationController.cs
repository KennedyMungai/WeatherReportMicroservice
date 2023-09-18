using Microsoft.AspNetCore.Mvc;

namespace CloudWeather.Precipitation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrecipitationController : ControllerBase
{
    [HttpGet("/observation/{zip}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByZip([FromBody] string zip, [FromQuery] int? days)
    {
        if (days is null || days > 30 || days < 0)
        {
            return await Task.FromResult(BadRequest("Please enter the value for days and it should be less than 30"));
        }

        if (zip is null)
        {
            return await Task.FromResult(BadRequest("The zip code has not been entered"));
        }

        return await Task.FromResult(Ok(zip));
    }
}