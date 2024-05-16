using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveChat.Context.Entities.Users;
using WaveChat.Services.Account.Data.DTO;

namespace WaveChat.Services.Account.Data.Mapper
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<User, AccountDto>().ReverseMap();
        }
    }
}
