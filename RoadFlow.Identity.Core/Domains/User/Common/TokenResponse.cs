namespace RoadFlow.Identity.Core.Domains.User.Common;

public record TokenResponse(
    string accessToken, DateTime expirationAccessTokenTime,
    string refreshToken, DateTime expirationRefreshTokenTime);