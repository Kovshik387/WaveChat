using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WaveChat.Context.Settings;

namespace WaveChat.Context;

public static class Bootstrapper
{
    public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = WaveChat.Settings.Settings.Load<DbSettings>("Database", configuration);
        services.AddSingleton(settings);
        
        var dbInitDelegate = DbContextOptionsFactory.Configure(settings.ConnectionString, settings.DatabaseType, true);

        services.AddDbContextFactory<CorporateMessengerContext>(dbInitDelegate);
        return services;
    }
}
