using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveChat.Context.Entities.Users;

namespace WaveChat.Services.Authorization.Data.Responses;

public class AuthorizationResponse //<T>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public string AccessToken { get; set; } = string.Empty;
    public AuthorizationResponse(User user, string accessToken)
    {
        this.Id = user.Id; this.AccessToken = accessToken; 
        this.Email = user.Email; this.Name = user.Name;
    }
}
