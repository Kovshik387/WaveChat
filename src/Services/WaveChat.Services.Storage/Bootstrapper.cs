using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveChat.Services.Storage.Infrastructure;
using WaveChat.Services.Storage.Services;

namespace WaveChat.Services.Storage
{
    public static class Bootstrapper
    {
        /// <summary>
        /// Регистрация сервисов
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IServiceCollection AddStorageService(this IServiceCollection services)
        {
            services.AddTransient<IStorageService, StorageService>();
            return services;
        }
    }
}

