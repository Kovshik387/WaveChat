using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveChat.Context.Settings;

namespace WaveChat.Context.Seeder.Seeds;

public static class DbSeeder
{
    private static IServiceScope ServiceScope(IServiceProvider serviceProvider)
    {
        return serviceProvider.GetService<IServiceScopeFactory>()!.CreateScope();
    }

    private static CorporateMessengerContext DbContext(IServiceProvider serviceProvider)
    {
        return ServiceScope(serviceProvider)
            .ServiceProvider.GetRequiredService<IDbContextFactory<CorporateMessengerContext>>().CreateDbContext();
    }

    public static void Execute(IServiceProvider serviceProvider)
    {
        Task.Run(async () =>
        {
            //await AddDemoData(serviceProvider);
            //await AddAdministrator(serviceProvider);
        })
            .GetAwaiter()
            .GetResult();
    }

    private static async Task AddDemoData(IServiceProvider serviceProvider)
    {
        //using var scope = ServiceScope(serviceProvider);
        //if (scope == null)
        //    return;

        //var settings = scope.ServiceProvider.GetService<DbSettings>();
        ////if (!(settings.Init?.AddDemoData ?? false))
        ////    return;

        //await using var context = DbContext(serviceProvider);

        ////if (await context.Books.AnyAsync())
        ////    return;

        ////await context.Books.AddRangeAsync(new DemoHelper().GetBooks);

        //await context.SaveChangesAsync();
    }

    private static async Task AddAdministrator(IServiceProvider serviceProvider)
    {
        //using var scope = ServiceScope(serviceProvider);
        //if (scope == null)
        //    return;

        //var settings = scope.ServiceProvider.GetService<DbSettings>();
        //if (!(settings.Init?.AddAdministrator ?? false))
        //    return;

        //var userAccountService = scope.ServiceProvider.GetService<IUserAccountService>();

        //if (!(await userAccountService.IsEmpty()))
        //    return;

        //await userAccountService.Create(new RegisterUserAccountModel()
        //{
        //    Name = settings.Init.Administrator.Name,
        //    Email = settings.Init.Administrator.Email,
        //    Password = settings.Init.Administrator.Password,
        //});
    }
}