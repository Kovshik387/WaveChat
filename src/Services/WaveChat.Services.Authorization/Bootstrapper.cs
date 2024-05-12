using Microsoft.Extensions.DependencyInjection;
using WaveChat.Services.Authorization.Infastructure;
using WaveChat.Services.Authorization.Services;

namespace WaveChat.Services.Authorization;
public static class Bootstrapper
{
    /// <summary>
    /// Регистрация сервисов
    /// </summary>
    /// <param name="service"></param>
    /// <returns></returns>
    public static IServiceCollection AddUserAccountService(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        return services;
    }
}
