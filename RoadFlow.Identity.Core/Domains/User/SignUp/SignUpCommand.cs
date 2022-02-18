using MediatR;
using RoadFlow.Identity.Core.Domains.User.Common;

namespace RoadFlow.Identity.Core.Domains.User.SignUp;

public record SignUpCommand(string Username, string Email, string Password) : IRequest<TokenResponse>;