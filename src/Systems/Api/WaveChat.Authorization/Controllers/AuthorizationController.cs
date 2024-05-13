using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WaveChat.Context;
using WaveChat.Services.Authorization.Data.DTO;
using WaveChat.Services.Authorization.Infastructure;
using WaveChat.Services.Authorization.Services;
using WaveChat.Services.Logger;
using IAuthorizationService = WaveChat.Services.Authorization.Infastructure.IAuthorizationService;

namespace WaveChat.Authorization.Controllers;

[ApiController]
[Route("v{version:apiVersion}/[Controller]/[Action]")]
public class AuthorizationController(IAppLogger appLogger, IAuthorizationService authorizationService)
    : ControllerBase
{
    private IAppLogger _logger = appLogger;
    private IAuthorizationService _authorizationService = authorizationService;
    /// <summary>
    /// Регистрация нового пользователя с выдачей access и refresh токенов
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ApiVersion("1.0")]
    public async Task<IActionResult> SignUp([FromBody] SignUpDTO model)
    {
        if (!ModelState.IsValid) { BadRequest(); }
        var result = await _authorizationService.SignUpAsync(model);
        return Ok(result);
    }
    /// <summary>
    /// Аутентификация и авторизация пользователя с выдачей access и refresh токенов
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiVersion("1.0")]
    public async Task<IActionResult> SignIn([FromQuery] SignInDTO model)
    {
        return Ok(await _authorizationService.SignInAsync(model));
    }

    [HttpGet]
    [ApiVersion("1.0")]
    public async Task<IActionResult> RefreshAccess(string refreshToken)
    {
        return Ok(await _authorizationService.GetAccessTokenAsync(refreshToken));
    }

    [HttpDelete]
    [ApiVersion("1.0")]
    [Authorize]
    public async Task<IActionResult> Logout(string refreshToken)
    {
        return Ok(await _authorizationService.Logout(refreshToken));
    }
}
