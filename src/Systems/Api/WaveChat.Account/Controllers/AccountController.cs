using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WaveChat.Services.Account.Data.DTO;
using WaveChat.Services.Account.Infrastructure;
using WaveChat.Services.Logger;

namespace WaveChat.Account.Controllers;
[ApiController]
[Route("v{version:apiVersion}/[Controller]/[Action]")]
public class AccountController(IAppLogger logger, IAccountService accountService) : ControllerBase
{
    private readonly IAppLogger _looger = logger;
    private readonly IAccountService _accountService = accountService;

    [Authorize]
    [HttpPatch]
    public async Task<IActionResult> UpdateUserData(AccountDetailsDTO accountDto)
    {
        await _accountService.UpdateAccountDataAsync(accountDto);
        return Ok("");
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAccountById(string id)
    {
        return Ok(await _accountService.GetAccountByIdAsync(id));
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAccountDetailsById(string id)
    {
        return Ok(await _accountService.GetAccountDetailsByIdAsync(id));
    }


}
