using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveChat.Services.Account.Infrastructure;
using WaveChat.Services.Account.Services;

namespace WaveChat.Services.Account
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddAccountService(this IServiceCollection services)
        {
            services.AddTransient<IAccountService, AccountService>();
            return services;
        }
    }
}
