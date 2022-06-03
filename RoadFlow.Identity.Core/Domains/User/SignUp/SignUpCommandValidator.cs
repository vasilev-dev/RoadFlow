using FluentValidation;
using RoadFlow.Identity.Core.Domains.User.Common;
using RoadFlow.Identity.Core.Domains.User.Common.Password;

namespace RoadFlow.Identity.Core.Domains.User.SignUp;

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