using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveChat.Context.Entities.Users;
using WaveChat.Services.Authorization.Data.DTO;

namespace WaveChat.Services.Authorization.Infastructure;

public interface IJwtUtils
{
    public AuthDTO GenerateJwtToken(Guid guid);
    public string? GetUserByRefreshToken(string refreshToken);
    public string? GetExpireTime(string refreshToken);
}
