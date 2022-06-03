using FluentValidation;

namespace RoadFlow.Identity.Core.Domains.User.Common;

public class UsernameValidator : AbstractValidator<string>
{
    public UsernameValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(32)
            .Matches("^[A-Za-z]").WithMessage("Username must starts with latin letter")
            .Matches("[A-Za-z0-9_]$").WithMessage("Username must contain only latin letters, digits and underscore");
    }
}