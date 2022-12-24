using FluentValidation;

namespace RoadFlow.Auth.Domain.ConfirmAccount.SendConfirmationCode;

public class SendConfirmationCodeCommandValidator : AbstractValidator<SendConfirmationCodeCommand>
{
    public SendConfirmationCodeCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();
    }
}