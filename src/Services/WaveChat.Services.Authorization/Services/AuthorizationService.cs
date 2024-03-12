using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WaveChat.Common.Validator;
using WaveChat.Context.Entities.Users;
using WaveChat.Services.Authorization.Data.DTO;
using WaveChat.Services.Authorization.Data.Responses;
using WaveChat.Services.Authorization.Infastructure;

namespace WaveChat.Services.Authorization.Services;
/// <summary>
/// Реализация интферейса <see cref="IAuthorizationService"/> аутентификации
/// </summary>
/// <param name="mapper"></param>
/// <param name="context"></param>
public class AuthorizationService(IMapper mapper, UserManager<User> userManager)
    //IModelValidator<SignUpValidator> modelValidator)
    : IAuthorizationService
{
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<User> _userManager = userManager;
    //private readonly IModelValidator<SignUpValidator> _modelValidator = modelValidator;
    public async Task<bool> IsEmptyAsync()
    {
       return !(await _userManager.Users.AnyAsync());
    }

    public async Task<AuthorizationResponse> SignInAsync(SignInDTO model)
    {
        var user = await _userManager.FindByEmailAsync(model.UserName);

        return _mapper.Map<AuthorizationResponse>(model);
    }

    public async Task<AuthorizationResponse> SignUpAsync(SignUpDTO model)
    {
        //_modelValidator.Check(model);

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
