using FluentValidation;
using RoadFlow.Auth.Domain.Common.Validators;

namespace RoadFlow.Auth.Domain.SIgnUp;

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotNull()
            .SetValidator(new UsernameValidator());

        RuleFor(x => x.Email)
            .NotNull()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotNull()
            .SetValidator(new PasswordValidator());
    }
}