using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using WaveChat.Common.Validator;
using WaveChat.Context.Entities.Users;
using WaveChat.Services.Authorization.Data.DTO;
using WaveChat.Services.Authorization.Data.Responses;
using WaveChat.Services.Authorization.Infastructure;
using WaveChat.Services.Authorization.Services.Validators;
using WaveChat.Services.Settings;
using WaveChat.Settings;

namespace WaveChat.Services.Authorization.Services;
/// <summary>
/// Реализация интферейса <see cref="IAuthorizationService"/> аутентификации
/// </summary>
/// <param name="mapper"></param>
/// <param name="context"></param>
public class AuthorizationService(IMapper mapper, UserManager<User> userManager,
    IModelValidator<SignUpDTO> signUpValidator,  IModelValidator<SignInDTO> signInValidator,
    IdentitySettings identity)
    : IAuthorizationService
{
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<User> _userManager = userManager;
    private readonly IModelValidator<SignUpDTO> _modelValidator = signUpValidator;
    private readonly IModelValidator<SignInDTO> _signInValidator = signInValidator;
    private readonly IdentitySettings _identity = identity;  
    public async Task<bool> IsEmptyAsync()
    {
       return !(await _userManager.Users.AnyAsync());
    }

    public async Task<AuthenticationResponse> SignInAsync(SignInDTO model)
    {
        _signInValidator.Check(model);

        // TO DO скрыть
        var url = $"http://host.docker.internal:5026/connect/token";
        //var url = _identity.Url;
        var request_body = new[]
        {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("client_id", "frontend"),
            new KeyValuePair<string, string>("client_secret", "A3F0811F2E934C4F1114CB693F7D785E"),
            new KeyValuePair<string, string>("username", model.UserName),
            new KeyValuePair<string, string>("password", model.Password!)
        };
        
        var requestContent = new FormUrlEncodedContent(request_body);

        var httpClient = new HttpClient() { BaseAddress = new Uri("http://localhost:8080")};

        var response = await httpClient.PostAsync(url, requestContent);

        var content = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<AuthenticationResponse>(content, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true}) ?? new AuthenticationResponse();

        result.Successful = response.IsSuccessStatusCode;

        if (!response.IsSuccessStatusCode)
        {
            return result;
        }

        return _mapper.Map<AuthenticationResponse>(result);
    }

    public async Task<AuthorizationResponse> SignUpAsync(SignUpDTO model)
    {
        _modelValidator.Check(model);

        var user = await _userManager.FindByEmailAsync(model.Email);
        
        if (user != null) throw new Exception($"User account with email {model.Email} already exist.");

        user = new User()
        {
            Email = model.Email,
            UserName = model.UserName,

            PhoneNumber = null,
            PhoneNumberConfirmed = false,

            Name = model.Name,
            Surname = model.Surname,
            Lastname = model.LastName,

            Registrationdate = DateOnly.FromDateTime(DateTime.UtcNow),
        };

        var result = await _userManager.CreateAsync(user,model.Password);
        if (!result.Succeeded) 
            throw new Exception($"Creating user account is wrong. " +
                $"{string.Join(", ", result.Errors.Select(s => s.Description))}");

        return _mapper.Map<AuthorizationResponse>(user);
    }
}
