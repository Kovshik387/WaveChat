using Microsoft.Extensions.DependencyInjection;
using WaveChat.Services.Message.Infrastructure;
using WaveChat.Services.Message.Services;

namespace WaveChat.Services.Message
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddMessageService(this IServiceCollection services)
            => services.AddTransient<IMessageService,MessageService>();
    }
}
