using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using WaveChat.Common.Extensions;
using WaveChat.Context;
using WaveChat.Context.Entities.Messages;
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
        _logger.LogInformation($"Письмо отправлено: {message.UidChannel}");

        var chat = await _context.Channels.Where(x => x.Uid.Equals(message.UidChannel)).FirstOrDefaultAsync();
        if (chat is null) throw new Exception("Нет чата");

        var idChannel = await _context.Channels.FirstAsync(x => x.Uid.Equals(message.UidChannel));
        var idUser = await _context.Users.FirstAsync(x => x.Uid.Equals(message.UidUser));

        chat.Messages.Add(new Context.Entities.Messages.Message()
        {
            Content = message.Content,
            Idchannel = idChannel.Id,
            Senddate = message.SendDate,
            Iduser = idUser.Id,
            Isread = true,
            
        });

        await _context.SaveChangesAsync();
    }

    public async Task<IList<ChatDTO>?> GetUserChatsAsync(string idUser)
    {
        var temp = await _context.Users.FirstAsync(x => x.Uid == Guid.Parse(idUser));

        var anotherUser = await _context.Userschannels.Include(x => x.Channel).ThenInclude(x => x.Messages).Where(x => x.Userid == temp.Id)
            .Select(x => new ChatDTO
            {
                MyMessage = false,
                Uid = x.Channel!.Uid,
                Name = x.Channel.Name,
                Users = _mapper.Map<IList<UserDTO>>(x.Channel!.Userschannels.
                    Where(x => x.Userid != temp.Id).Select(x => x.User).ToList()),
                Url = "",
                LastMessage = x.Channel.Messages.OrderByDescending(cd => cd.Id).First().Content
            })
            .ToListAsync();

        return _mapper.Map<IList<ChatDTO>>(anotherUser);
    }

    public async Task<List<UserDTO>> GetAccountByUserNameAsync(string userName, string uid)
    {
        var user = await _context.Users
            .Where(x => x.Uid.Equals(Guid.Parse(uid)))
            .FirstOrDefaultAsync();

        if (user is null)
            throw new Exception("User not found");

        var result = new List<UserDTO>();

        if (string.IsNullOrWhiteSpace(userName))
            return result;

        var users = await _context.Users
            .Where(x => EF.Functions.Like(x.Username, $"%{userName}%"))
            .ToListAsync();

        var userChatIds = await _context.Userschannels
            .Where(x => x.Userid.Equals(user.Id))
            .Select(x => x.Channelid)
            .ToListAsync();

        if (!userChatIds.Any())
        {
            foreach (var item in users)
            {
                result.Add(_mapper.Map<UserDTO>(item));
            }
            return result;
        }

        foreach (var item in users)
        {
            bool chatExists = await _context.Userschannels
                .AnyAsync(x => x.Userid.Equals(item.Id) && userChatIds.Contains(x.Channelid));

            if (!chatExists)
            {
                result.Add(_mapper.Map<UserDTO>(item));
            }
        }

        return result;
    }
    public async Task<IList<MessageDTO>> GetMessageByChatAsync(string chatId)
    {
        _logger.LogInformation($"{chatId}");
        var result = await _context.Channels.Where(x => x.Uid.Equals(Guid.Parse(chatId))).FirstOrDefaultAsync();

        return _mapper.Map<IList<MessageDTO>>(result.Messages);
    }

    public async Task<ChatDTO> NewChatAsync(string idUser,string idAnotherUser)
    {
        var id = await _context.Users.FirstOrDefaultAsync(x => x.Uid.Equals(Guid.Parse(idUser)));
        var idAnother = await _context.Users.FirstOrDefaultAsync(x => x.Uid.Equals(Guid.Parse(idAnotherUser)));

        var channel = await _context.Channels.AddAsync(new Channel()
        {
            Uid = Guid.NewGuid(),
            Name = "",
            Userschannels = new List<Userschannel>()
            {
               new Userschannel() { Userid =  id!.Id, Uid = Guid.NewGuid()},
               new Userschannel() { Userid = idAnother!.Id, Uid = Guid.NewGuid()}
            }
        });
        await _context.SaveChangesAsync();

        var result = new ChatDTO()
        {
            MyMessage = false,
            Uid = channel.Entity.Uid,
            Name = channel.Entity.Name,
            Users = _mapper.Map<IList<UserDTO>>(channel.Entity.Userschannels.
                    Where(x => x.Userid != id.Id).Select(x => x.User).ToList()),
            Url = "",
            LastMessage = ""
        };


        return result;
    }
}
