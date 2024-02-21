using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WaveChat.Services.Logger;

namespace WaveChat.Authorization.Controllers;

[ApiController]
[Route("v{version:apiVersion}/[Controller]/[Action]")]
public class AuthorizationController : ControllerBase
{
    private IAppLogger _logger;
    public AuthorizationController(IAppLogger appLogger)
    {
        _logger = appLogger;
    }

    [HttpGet]
    [ApiVersion("1.0")]

    public IActionResult Index([FromQuery] int a)
    {
        return Ok(a);
    }
}
