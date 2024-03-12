using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveChat.Services.Authorization.Data.DTO;

namespace WaveChat.Services.Authorization.Services.Validators
{
    public class SignUpValidator : AbstractValidator<SignUpDTO>
    {
        public SignUpValidator()
        {
            RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("User name is required.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(50).WithMessage("Password is long.");
        }
    }
}
