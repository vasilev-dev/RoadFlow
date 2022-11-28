using FluentValidation;
using RoadFlow.Auth.Domain.Common.Validators;

namespace RoadFlow.Auth.Domain.SignIn;

public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotNull()
            .NotEmpty()
            .SetValidator(new UsernameValidator());

        RuleFor(x => x.Password)
            .NotNull()
            .SetValidator(new PasswordValidator());
    }
}