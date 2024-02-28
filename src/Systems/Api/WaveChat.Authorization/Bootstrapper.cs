namespace WaveChat.Authorization;

using WaveChat.Services.Logger;
using WaveChat.Services.Settings;
using WaveChat.Context.Seeder;

public static class Bootstrapper
{
    public static IServiceCollection RegisterService(this IServiceCollection service)
    {
        service.AddMainSettings()
            .AddLogSettings()
            .AddAppLogger()
            .AddSwaggerSettings()
            //.AddDbSeeder()
            ;

        return service;
    }
}
