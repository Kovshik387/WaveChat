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
public class AuthorizationController(IAppLogger appLogger, IDbContextFactory<CorporateMessengerContext> dbContext) : ControllerBase
{
    private IAppLogger _logger = appLogger;
    private IDbContextFactory<CorporateMessengerContext> _dbContext = dbContext;
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ApiVersion("1.0")]
    public async Task<IActionResult> GetList()
    {
        using var context = await _dbContext.CreateDbContextAsync();
        var list = await context.Rolestypes.Select(x => x.Rolename).ToListAsync();
        return Ok(list);
    }
}
