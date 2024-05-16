using WaveChat.Services.Authorization.Infastructure;
using WaveChat.Services.Authorization.Services;
using WaveChat.Services.Authorization.Utils;

namespace WaveChat.Message.Configuration;

public static class JwtConfiguration
{
    public static IServiceCollection AddAppJwt(this IServiceCollection services)
    {
        services.AddScoped<IConnectionWithStorageApi, ConnectionWithStorageApi>();
        return services.AddScoped<IJwtUtils,JwtUtils>();

    }
}
