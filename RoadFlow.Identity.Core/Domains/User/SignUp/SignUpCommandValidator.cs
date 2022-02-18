using FluentValidation;

namespace RoadFlow.Identity.Core.Domains.User.SignUp;

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(32);

        RuleFor(x => x.Email)
            .NotNull()
            .EmailAddress();

        /*
         * minimum 8 characters
         * maximum 32 characters
         * at least one uppercase letter
         * one lowercase letter
         * one number
         * one special character
         */
        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,32}$");
    }
}