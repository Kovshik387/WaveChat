using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using WaveChat.Context.Entities.Messages;
using WaveChat.Context.Entities.Users;
using WaveChat.Services.Message.Infrastructure;

namespace WaveChat.Message.Hubs;

public class ChatHub(IMessageService messageService,ILogger<ChatHub> logger) : Hub
{
    private readonly IMessageService _messageService = messageService;
    private readonly ILogger<ChatHub> _logger = logger;
    [Authorize]
    public async Task JoinChat(string chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId,chatId);

        await Clients.Group(chatId).SendAsync("JoinGroup",chatId);
    }
    [Authorize]
    public async Task SendMessage(string userId, string content, string chatId)
    {
        await Clients.Group(chatId).SendAsync("ReceiveMessage", userId, content);
        await _messageService.AddMessageAsync(new Services.Message.Data.DTO.MessageDTO()
        {
            Content = content,
            SendDate = DateTime.Now,
            UidChannel = Guid.Parse(chatId),
            UidUser = Guid.Parse(userId)
        });
    }

}
