using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class HeartBeatController : ControllerBase
{
    [HttpGet]
    [Route("live")]
    public IActionResult Get()
    {
        return Ok("Ok");
    }
}