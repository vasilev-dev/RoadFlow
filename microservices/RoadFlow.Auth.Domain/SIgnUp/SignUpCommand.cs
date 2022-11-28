using MediatR;

namespace RoadFlow.Auth.Domain.SIgnUp;

public record SignUpCommand(string Username, string Email, string Password) : IRequest<Unit>;