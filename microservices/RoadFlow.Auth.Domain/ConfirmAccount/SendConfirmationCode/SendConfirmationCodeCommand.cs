using MediatR;

namespace RoadFlow.Auth.Domain.ConfirmAccount.SendConfirmationCode;

public record SendConfirmationCodeCommand(string Email) : IRequest<Unit>;
