using AutoMapper;
using WaveChat.Context.Entities.Users;
using WaveChat.Services.Authorization.Data.DTO;
using WaveChat.Services.Authorization.Data.Responses;

namespace WaveChat.Services.Authorization.Data.Mapper;

public class AuthorizationProfile : Profile
{
    public AuthorizationProfile()
    {
        CreateMap<User, AuthorizationResponse>()
            //.ForMember(x => x.Guid, y => y.MapFrom(s => s.Id))
            .ReverseMap();
        CreateMap<AuthorizationResponse, SignInDTO>().ReverseMap();
    }
}
