namespace WaveChat.Authorization;

using WaveChat.Services.Logger;
using WaveChat.Services.Settings;

public static class Bootstrapper
{
    public static IServiceCollection RegisterService(this IServiceCollection service)
    {
        service.AddMainSettings()
            .AddLogSettings()
            .AddAppLogger()
            .AddSwaggerSettings()
            ;

        return service;
    }
}
