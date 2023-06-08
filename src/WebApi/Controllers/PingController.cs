using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;

namespace WebApi.Controllers;

public class PingController : ControllerBase
{
    [HttpGet, Route(ApiRoutes.Ping.HealthCheck)]
    public IActionResult Get()
    {
        return Ok("Dogs house service. Version 1.0.1");
    }
}