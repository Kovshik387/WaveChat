using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WaveChat.Common.Extensions;
using WaveChat.Context;
using WaveChat.Services.Message.Data.DTO;
using WaveChat.Services.Message.Infrastructure;

namespace WaveChat.Services.Message.Services;

public class MessageService(ILogger<MessageService> logger,CorporateMessengerContext context,IMapper mapper) : IMessageService
{
    private readonly ILogger<MessageService> _logger = logger;
    private readonly CorporateMessengerContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task AddMessageAsync(MessageDTO message)
    {
        _logger.LogInformation($"Письмо отправлено: {message}");

        var chat = await _context.Channels.Where(x => x.Uid.Equals(message.UidChannel)).FirstOrDefaultAsync();
        if (chat is null) throw new Exception("Нет чата");
        
        chat.Messages.Add(_mapper.Map<WaveChat.Context.Entities.Messages.Message>(message));

        await _context.SaveChangesAsync();
    }

    public async Task<IList<MessageDTO>> GetMessageByChatAsync(string chatId)
    {
        _logger.LogInformation($"{chatId}");
        return _mapper.Map<IList<MessageDTO>>(await _context.Messages.Where(x => x.Uid.Equals(chatId)).ToListAsync());
    }
}
