namespace RoadFlow.Identity.Core.Domains.Token;

public interface ITokenService
{
    (string accessToken, DateTime expirationAccessTokenTime) GenerateAccessToken(string id, string email, string username, string role);
    (string refreshToken, DateTime expirationRefreshTokenTime) GenerateRefreshToken();
}