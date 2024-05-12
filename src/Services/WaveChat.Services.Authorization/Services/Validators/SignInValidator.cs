using FluentValidation;
using WaveChat.Services.Authorization.Data.DTO;

namespace WaveChat.Services.Authorization.Services.Validators;

public class SignInValidator : AbstractValidator<SignInDTO>
{
    public SignInValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Empty email");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Empty password");
    }
}
