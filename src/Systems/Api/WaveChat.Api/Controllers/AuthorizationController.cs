using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WaveChat.Context;
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

    public IActionResult GetList()
    {
        return Ok();
    }
}
