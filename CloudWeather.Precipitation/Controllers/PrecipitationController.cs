using Microsoft.AspNetCore.Mvc;

namespace CloudWeather.Precipitation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrecipitationController : ControllerBase
{
    [HttpGet("/observation/{zip}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByZip([FromBody] string zip)
    {
        return await Task.FromResult(Ok(zip));
    }
}