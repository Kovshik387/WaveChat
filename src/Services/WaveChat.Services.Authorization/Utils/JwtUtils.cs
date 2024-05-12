using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WaveChat.Context;
using WaveChat.Context.Entities.Users;
using WaveChat.Services.Authorization.Data.DTO;
using WaveChat.Services.Authorization.Infastructure;
using WaveChat.Services.Settings;
using WaveChat.Settings;

namespace WaveChat.Services.Authorization.Utils;

public class JwtUtils(CorporateMessengerContext context, AuthSettings settings, ILogger<JwtUtils> logger) : IJwtUtils
{
    private readonly CorporateMessengerContext _context = context;
    private readonly AuthSettings _setting = settings;
    private readonly ILogger<JwtUtils> _logger = logger;

    public AuthDTO GenerateJwtToken(Guid guid)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>() { new Claim(ClaimTypes.Name, guid.ToString()) };

        var accessTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_setting.AccessTokenLifetimeMinutes),
            Issuer = "Api",
            SigningCredentials = new SigningCredentials(_setting.SymmetricSecurityKeyAccess,
                SecurityAlgorithms.HmacSha256),
        };

        var refreshTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(_setting.RefreshTokenLifetimeDays),
            Issuer = "Api",
            SigningCredentials = new SigningCredentials(_setting.SymmetricSecurityKeyRefresh,
                SecurityAlgorithms.HmacSha256Signature),
        };

        var authDTO = new AuthDTO()
        {
            Id = guid,
            AccessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(accessTokenDescriptor)),
            RefreshToken = tokenHandler.WriteToken(tokenHandler.CreateToken(refreshTokenDescriptor))
        };

        return authDTO;
    }

    public string? GetUserByRefreshToken(string refreshToken)
    {
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = _setting.Issuer,
            ValidateAudience = true,
            ValidAudience = _setting.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _setting.SymmetricSecurityKeyRefresh
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            ClaimsPrincipal claims = tokenHandler.ValidateToken(refreshToken, tokenValidationParameters, out var validatedToken);
            return claims.Claims.First(x => x.Type.Equals(ClaimTypes.Name)).Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }
}
