using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using WaveChat.Common.Validator;
using WaveChat.Context;
using WaveChat.Context.Entities.Users;
using WaveChat.Services.Authorization.Data.DTO;
using WaveChat.Services.Authorization.Data.Responses;
using WaveChat.Services.Authorization.Infastructure;

namespace WaveChat.Services.Authorization.Services;
/// <summary>
/// Реализация интерфейса <see cref="IAuthorizationService"/> аутентификации
/// </summary>
/// <param name="mapper"></param>
/// <param name="context"></param>
public class AuthorizationService(IMapper mapper, IValidator<SignUpDTO> signUpValidator, ILogger<AuthorizationService> logger,
    IValidator<SignInDTO> signInValidator, IConnectionWithStorageApi connectionWithStorageApi, CorporateMessengerContext context, IJwtUtils jwtUtils)
    : IAuthorizationService
{
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<SignUpDTO> _signUpValidator = signUpValidator;
    private readonly IValidator<SignInDTO> _signInValidator = signInValidator;
    private readonly CorporateMessengerContext _context = context;
    private readonly IJwtUtils _jwtUtils = jwtUtils;
    private readonly ILogger<AuthorizationService> _logger = logger;
    private readonly IConnectionWithStorageApi _connection = connectionWithStorageApi;
    public async Task<bool> IsEmptyAsync()
    {
        return !(await _context.Users.AnyAsync());
    }

    public async Task<AuthResponse<AuthDTO>> SignInAsync(SignInDTO model)
    {
        var validated = await _signInValidator.ValidateAsync(model);
        if (!validated.IsValid)
        {
            return new AuthResponse<AuthDTO>()
            {
                Data = null,
                ErrorMessage = validated.Errors.First().ErrorMessage
            };
        }

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email.ToUpper());

        if (user is null){
            return new AuthResponse<AuthDTO>()
            {
                Data = null,
                ErrorMessage = "Invalid email."
            };
        }

        if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Passwordhash))
        {
            return new AuthResponse<AuthDTO>()
            {
                Data = null,
                ErrorMessage = "Invalid password."
            };
        }

        var authResult = _jwtUtils.GenerateJwtToken(user.Uid); authResult.Name = user.Name;

        user.RefreshToken = authResult.RefreshToken;
        _context.Users.Update(user); await _context.SaveChangesAsync();

        return new AuthResponse<AuthDTO>()
        {
            Data = authResult,
            ErrorMessage = ""
        };
    }

    public async Task<AuthResponse<AuthDTO>> SignUpAsync(SignUpDTO model)
    {
        var validated = await _signUpValidator.ValidateAsync(model);
        if (!validated.IsValid)
        {
            return new AuthResponse<AuthDTO>()
            {
                Data = null,
                ErrorMessage = validated.Errors.First().ErrorMessage
            };
        }

        var userExist = await _context.Users.Where(x => x.Email == model.Email).FirstOrDefaultAsync();

        if (userExist != null) return new AuthResponse<AuthDTO>()
        {
            Data = null,
            ErrorMessage = $"User account with email {model.Email} already exist."
        };

        var userName = await _context.Users.Where(x => x.Username == model.UserName).FirstOrDefaultAsync();

        if (userName != null) return new AuthResponse<AuthDTO>()
        {
            Data = null,
            ErrorMessage = $"User account with username {model.UserName} already exist."
        };

        var userGuid = Guid.NewGuid();

        var token = _jwtUtils.GenerateJwtToken(userGuid);
        var user = new User()
        {
            Uid = userGuid,
            Username = model.UserName,
            Email = model.Email.ToUpper(),
            Passwordhash = BCrypt.Net.BCrypt.HashPassword(model.Password, 10),
            Name = model.Name,
            Surname = model.Surname,
            Lastname = model.LastName,
            RefreshToken = token.RefreshToken,
            Registrationdate = DateOnly.FromDateTime(DateTime.UtcNow),
        };

        var result =  await _context.Users.AddAsync(user); await _context.SaveChangesAsync();

        if (result is null)
            throw new Exception($"Creating user account is wrong.");

        return new AuthResponse<AuthDTO>()
        {
            Data = new AuthDTO()
            { RefreshToken = token.RefreshToken,
                AccessToken = token.AccessToken,
                Id = userGuid,
                Name = user.Name
            },
            ErrorMessage = ""
        };
    }

    public async Task<AuthResponse<AuthDTO>> GetAccessTokenAsync(string refreshToken)
    {
        var idUser = _jwtUtils.GetUserByRefreshToken(refreshToken);

        if (idUser is null)
        {
            return new AuthResponse<AuthDTO>()
            {
                Data = null,
                ErrorMessage = "Invalid Refresh Token."
            };
        }

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Uid == Guid.Parse(idUser));

        if (user is null || user.RefreshToken != refreshToken)
        {
            return new AuthResponse<AuthDTO>()
            {
                Data = null,
                ErrorMessage = "Invalid Refresh Token."
            };
        }

        var tokens = _jwtUtils.GenerateJwtToken(Guid.Parse(idUser));

        return new AuthResponse<AuthDTO>()
        {
            Data = new AuthDTO()
            {
                Id = Guid.Parse(idUser),
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken,
            },
            ErrorMessage = ""
        };
    }

    public async Task<AuthResponse<object>> Logout(string refreshToken)
    {
        var idUser = _jwtUtils.GetUserByRefreshToken(refreshToken);
        if (idUser is null)
        {
            return new AuthResponse<object>()
            {
                Data = null,
                ErrorMessage = "Invalid Refresh Token."
            };
        }

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Uid == Guid.Parse(idUser));
        if (user is null || user.RefreshToken != refreshToken)
        {
            return new AuthResponse<object>()
            {
                Data = null,
                ErrorMessage = "Invalid Refresh Token."
            };
        }

        user.RefreshToken = ""; _context.Users.Update(user); await _context.SaveChangesAsync();
        return new AuthResponse<object>
        {
            Data = null,
            ErrorMessage = ""
        };
    }

}
