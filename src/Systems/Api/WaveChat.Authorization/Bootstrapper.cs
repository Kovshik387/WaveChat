namespace WaveChat.Authorization;

using WaveChat.Services.Logger;
using WaveChat.Services.Settings;
using WaveChat.Services.Authorization;
using WaveChat.Authorization.Configuration;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration = null)
    {
        services.AddMainSettings()
            .AddLogSettings()
            .AddIdentitySettings()
            .AddAppLogger()
            .AddSwaggerSettings()
            .AddUserAccountService()
            //.AddDbSeeder()
            ;

        return services;
    }
}
