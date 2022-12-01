using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

[Route("api/[controller]")]
[ApiController]
[ExcludeFromCodeCoverage]
public class HeartBeatController : ControllerBase
{
    [HttpGet]
    [Route("live")]
    public IActionResult Get()
    {
        return Ok("Ok");
    }
}