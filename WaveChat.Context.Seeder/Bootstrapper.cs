using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WaveChat.Context.Seeder;

public static class Bootstrapper
{
    public static IServiceCollection AddDbSeeder(this IServiceCollection services, IConfiguration configuration = null)
    {
        return services;
    }
}
