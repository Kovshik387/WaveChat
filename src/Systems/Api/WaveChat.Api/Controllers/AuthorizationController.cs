using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace WaveChat.Authorization.Controllers;

[ApiController]
[Route("v{version:apiVersion}/[Controller]/[Action]")]
public class AuthorizationController : ControllerBase
{
    public AuthorizationController()
    {
        
    }

    /// <summary>
    /// Тест
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>

    [HttpGet]
    [ApiVersion("1.0")]
    
    public IActionResult Index([FromQuery]int a) => Ok(a);
}
