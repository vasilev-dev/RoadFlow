using FluentValidation;

namespace RoadFlow.Identity.Core.Domains.User.Common;

public class UsernameValidator : AbstractValidator<string>
{
    public UsernameValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(32);
    }
}