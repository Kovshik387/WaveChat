using WaveChat.Services.Settings;

namespace WaveChat.IdentityServer
{
    public static class Bootstrapper
    {
        public static IServiceCollection RegisterAppServices(this IServiceCollection services)
        {
            services.
                AddMainSettings().
                AddLogSettings();
            
            return services;    
        }
    }
}
