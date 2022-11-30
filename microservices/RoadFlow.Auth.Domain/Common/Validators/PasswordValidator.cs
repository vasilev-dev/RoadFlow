using FluentValidation;

namespace RoadFlow.Auth.Domain.Common.Validators;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(32)
            .Matches("[a-z]+")
            .Matches("[A-Z]+")
            .Matches("\\d+")
            .Matches("[@$!%*?&]+");
    }
}