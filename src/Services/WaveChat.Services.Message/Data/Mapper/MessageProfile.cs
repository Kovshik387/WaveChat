using AutoMapper;
using WaveChat.Context.Entities.Messages;
using WaveChat.Context.Entities.Users;
using WaveChat.Services.Message.Data.DTO;

namespace WaveChat.Services.Message.Data.Mapper;

public class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<MessageDTO, Context.Entities.Messages.Message>().ReverseMap()
            .ForMember("UidUser", opt => opt.MapFrom(c => c.IduserNavigation.Uid))
            .ForMember("Name", opt => opt.MapFrom(c => c.IduserNavigation.Name));
        CreateMap<ChatDTO, Channel>().ReverseMap();

        CreateMap<UserDTO, User>().ReverseMap();
    }
}
