using WaveChat.Authorization.Configuration;
using WaveChat.Services.Logger;
using WaveChat.Services.Settings.Settings;
using WaveChat.Services.Storage;

namespace WaveChat.Storage
{
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
                .AddStorageService()
                ;

            return services;
        }
    }
}
