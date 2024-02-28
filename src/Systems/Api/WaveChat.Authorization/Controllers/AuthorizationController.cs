using Asp.Versioning;
using Castle.Core.Logging;
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
    private IDbContextFactory<CorporateMessengerContext> _dbContext;
    public AuthorizationController(IAppLogger appLogger, IDbContextFactory<CorporateMessengerContext> dbContext)
    {
        _logger = appLogger; _dbContext = dbContext;
    }

    [HttpGet]
    [ApiVersion("1.0")]

    public async Task<IActionResult> GetList()
    {
        using var context = await _dbContext.CreateDbContextAsync();
        var list = await context.Rolestypes.Select(x => x.Rolename).ToListAsync();
        return Ok(list);
    }
}
