using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveChat.Services.Message.Data.DTO;

namespace WaveChat.Services.Message.Infrastructure;

public interface IMessageService
{
    public Task<IList<MessageDTO>> GetMessageByChatAsync(string chatId);
    public Task AddMessageAsync(MessageDTO message); 
}
