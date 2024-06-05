using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WaveChat.Services.Logger;
using WaveChat.Services.Message.Infrastructure;

namespace WaveChat.Message.Controllers;

[ApiController]
[Route("v{version:apiVersion}/[Controller]/[Action]")]
public class ChatController(IMessageService messageService,IAppLogger logger) : ControllerBase
{
    private readonly IMessageService _messageService = messageService;
    private readonly IAppLogger _logger = logger;

    [HttpGet]
    [Authorize]
    [ApiVersion("1.0")]
    public async Task<IActionResult> GetChatMessage(string idChat)
    {
        return Ok(await _messageService.GetMessageByChatAsync(idChat));
    }

    [HttpGet]
    [Authorize]
    [ApiVersion("1.0")]
    public async Task<IActionResult> GetUserChats(string idUser)
    {
        return Ok(await _messageService.GetUserChatsAsync(idUser));
    }

    [HttpGet]
    [Authorize]
    [ApiVersion("1.0")]
    public async Task<IActionResult> GetAccountByUserNameAsync(string userName, string id)
    {
        return Ok(await _messageService.GetAccountByUserNameAsync(userName,id));
    }
}
