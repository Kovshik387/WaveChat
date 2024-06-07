namespace WaveChat.Message;

using WaveChat.Services.Logger;
using WaveChat.Services.Settings.Settings;
using WaveChat.Services.Authorization;
using WaveChat.Message.Configuration;
using WaveChat.Message.Hubs;
using WaveChat.Services.Message;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration = null)
    {
        services.AddMainSettings()
            .AddLogSettings()
            .AddIdentitySettings()
            .AddAppLogger()
            .AddSwaggerSettings()
            .AddAppJwt()
            .AddUserAccountService()
            .AddPathSettings()
            .AddMessageService()
            .AddSignalR()
            ;

        return services;
    }
}
