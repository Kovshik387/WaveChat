using WaveChat.Services.Authorization.Infastructure;
using WaveChat.Services.Authorization.Utils;

namespace WaveChat.Authorization.Configuration;

public static class JwtConfiguration
{
    public static IServiceCollection AddAppJwt(this IServiceCollection services)
    {
        return services.AddScoped<IJwtUtils,JwtUtils>();
    }
}
