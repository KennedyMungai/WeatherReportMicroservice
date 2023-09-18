using Microsoft.AspNetCore.Mvc;

namespace CloudWeather.Precipitation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrecipitationController : ControllerBase
{
    [HttpGet("/observation/{zip:string}")]
    public async Task<IActionResult> GetByZip([FromBody] string zip)
    {
        return async Task.FromResult(Ok(zip));
    }
}