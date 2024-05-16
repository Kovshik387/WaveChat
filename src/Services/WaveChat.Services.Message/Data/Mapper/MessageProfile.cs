using AutoMapper;
using WaveChat.Context.Entities.Messages;
using WaveChat.Services.Message.Data.DTO;

namespace WaveChat.Services.Message.Data.Mapper;

public class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<Context.Entities.Messages.Message,MessageDTO>().ReverseMap();
        CreateMap<Channel,ChatDTO>().ReverseMap();
    }
}
