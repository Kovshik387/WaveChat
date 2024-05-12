namespace WaveChat.Authorization.Configuration;

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
                    ValidIssuer = "Api",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = settings.SymmetricSecurityKeyAccess,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                };
                options.Audience = "Authorization_Api";;
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
