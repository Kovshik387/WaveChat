using WaveChat.Services.Settings.Settings;
using WaveChat.Services.Account;
using WaveChat.Services.Logger;
using WaveChat.Authorization.Configuration;

namespace WaveChat.Account;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration = null)
    {
        services.AddMainSettings()
            .AddLogSettings()
            .AddIdentitySettings()
            .AddAppLogger()
            .AddSwaggerSettings()
            .AddAccountService()
            .AddAppJwt()
            ;

        return services;
    }
}
