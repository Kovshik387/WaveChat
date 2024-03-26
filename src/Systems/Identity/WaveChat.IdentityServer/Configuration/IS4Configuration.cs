namespace WaveChat.IdentityServer.Configuration;

using WaveChat.Context;
using Microsoft.AspNetCore.Identity;
using WaveChat.Context.Entities.Users;
using WaveChat.Identity.Configuration;

public static class IS4Configuration
{
    public static IServiceCollection AddIS4(this IServiceCollection services)
    {
        services
            .AddIdentity<User, IdentityRole<Guid>>(opt =>
            {
                opt.Password.RequiredLength = 0;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<CorporateMessengerContext>()
            .AddUserManager<UserManager<User>>()
            .AddDefaultTokenProviders()
            ;

        services
            .AddIdentityServer()

            .AddAspNetIdentity<User>()

            .AddInMemoryApiScopes(AppApiScopes.ApiScopes)
            .AddInMemoryClients(AppClients.Clients)
            .AddInMemoryApiResources(AppResources.Resources)
            .AddInMemoryIdentityResources(AppIdentityResources.Resources)

            .AddDeveloperSigningCredential()
            ;

        return services;
    }

    public static IApplicationBuilder UseIS4(this IApplicationBuilder app)
    {
        app.UseIdentityServer();

        return app;
    }
}

