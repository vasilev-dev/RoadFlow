namespace RoadFlow.Auth.Domain.Common.Responses;

public record TokenResponse(
    string AccessToken, 
    string RefreshToken,
    DateTime ExpirationAccessTokenTime,
    DateTime ExpirationRefreshTokenTime);