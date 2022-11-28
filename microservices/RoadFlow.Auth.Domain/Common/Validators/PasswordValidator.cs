using FluentValidation;

namespace RoadFlow.Auth.Domain.Common.Validators;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(64)
            .Matches("[a-z]+")
            .Matches("[A-Z]+")
            .Matches("[0-9]+")
            .Matches("\\W+");
    }
}