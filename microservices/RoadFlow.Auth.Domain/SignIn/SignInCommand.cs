using MediatR;
using RoadFlow.Auth.Domain.Common.Responses;

namespace RoadFlow.Auth.Domain.SignIn;

public record SignInCommand(string Username, string Password) : IRequest<TokenResponse>;