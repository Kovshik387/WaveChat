﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using WaveChat.Context.Entities.Messages;
using WaveChat.Context.Entities.Users;
using WaveChat.Services.Message.Data.DTO;
using WaveChat.Services.Message.Infrastructure;

namespace WaveChat.Message.Hubs;

public interface IChatHub
{
    public Task ReceiveMessage(string userId, string content, string chatId);
}

public class ChatHub(IMessageService messageService,ILogger<ChatHub> logger) : Hub//<IChatHub>
{
    private readonly IMessageService _messageService = messageService;
    private readonly ILogger<ChatHub> _logger = logger;
    //[Authorize]
    public async Task JoinChat(string chatId)
    {
        Console.WriteLine("chat connect" + chatId   );
        await Groups.AddToGroupAsync(Context.ConnectionId,chatId);

        await Clients.Group(chatId).SendAsync("JoinGroup",chatId);
        _logger.LogInformation($"Join chat");
        Console.WriteLine($"Join chat");
    }
    //[Authorize]
    public async Task SendMessage(string userId, string content, string chatId,string userName = "пользователь")
    {
        await _messageService.AddMessageAsync(new Services.Message.Data.DTO.MessageDTO()
        {
            Content = content,
            SendDate = DateTime.UtcNow,
            UidChannel = Guid.Parse(chatId),
            UidUser = Guid.Parse(userId)
        });

        var messageDto = new
        {
            Content = content,
            SendDate = DateTime.UtcNow,
            UidChannel = Guid.Parse(chatId),
            UidUser = Guid.Parse(userId),
            Name = userName
        };

        await Clients.Group(chatId).SendAsync("ReceiveMessage", messageDto);
        await Clients.All.SendAsync("NewMessageNotification", messageDto);

        _logger.LogInformation($"Message sent to chat: {chatId}");
    }

}
