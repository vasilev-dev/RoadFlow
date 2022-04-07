using MediatR;
using RoadFlow.Identity.Core.Domains.User.Common;

namespace RoadFlow.Identity.Core.Domains.User.SignIn;

public record SignInCommand(string Email, string Password): IRequest<TokenResponse>;