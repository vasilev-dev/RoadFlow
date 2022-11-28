using FluentValidation;

namespace RoadFlow.Auth.Domain.Common.Validators;

public class UsernameValidator : AbstractValidator<string>
{
    public UsernameValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .Matches("^[A-Za-z0-9_.]+$")
            .MinimumLength(3)
            .MaximumLength(32);
    }
}