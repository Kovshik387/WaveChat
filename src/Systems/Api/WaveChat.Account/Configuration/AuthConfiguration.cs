namespace WaveChat.Account.Configuration;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using WaveChat.Context.Entities.Users;
using WaveChat.Context;
using WaveChat.Services.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;

public static class AuthConfiguration
{
    public static IServiceCollection AddAppAuth(this IServiceCollection services, AuthSettings settings)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = settings.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = settings.SymmetricSecurityKeyAccess,
                    ValidateAudience = true,
                    ValidAudience = settings.Audience,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                };
            });

        return services;
    }

    public static IApplicationBuilder UseAppAuth(this IApplicationBuilder app)
    {
        app.UseAuthentication();

        app.UseAuthorization();

        return app;
    }
}
