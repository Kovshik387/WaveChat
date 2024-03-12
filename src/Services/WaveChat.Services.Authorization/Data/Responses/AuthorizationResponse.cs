using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveChat.Services.Authorization.Data.Responses;

public class AuthorizationResponse //<T>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    //public T Data { get; set; } = default(T)!;
}
