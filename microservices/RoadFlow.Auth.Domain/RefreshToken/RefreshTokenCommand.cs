using MediatR;
using RoadFlow.Auth.Domain.Common.Responses;

namespace RoadFlow.Auth.Domain.RefreshToken;

public record RefreshTokenCommand(string RefreshToken) : IRequest<TokenResponse>;