using FluentValidation;

namespace RoadFlow.Auth.Domain.ConfirmAccount.ValidateConfirmationCode;

public class ValidateConfirmationCodeCommandValidator : AbstractValidator<ValidateConfirmationCodeCommand>
{
    public ValidateConfirmationCodeCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Code)
            .NotNull()
            .NotEmpty()
            .Matches("^\\d{6}$");
    }
}