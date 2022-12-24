using MediatR;

namespace RoadFlow.Auth.Domain.ConfirmAccount.ValidateConfirmationCode;

public record ValidateConfirmationCodeCommand(string Email, string Code) : IRequest<Unit>;