using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RoadFlow.Common.Configurations;

namespace RoadFlow.Identity.Core.Domains.Token;

public class TokenService : ITokenService
{
    private readonly JwtConfiguration _jwtConfiguration;

    public TokenService(JwtConfiguration jwtConfiguration)
    {
        _jwtConfiguration = jwtConfiguration;
    }
    
    public (string accessToken, DateTime expirationAccessTokenTime) GenerateAccessToken(string email, string username, string role)
    {
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Email, email),
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Role, role)
        };
        
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.IssuerSigningKey));
        
        var token = new JwtSecurityToken(
            _jwtConfiguration.ValidIssuer,
            _jwtConfiguration.ValidAudience,
            expires: DateTime.Now.AddMinutes(_jwtConfiguration.AccessTokenLifetimeInMinutes),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        
        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
        
        return (accessToken, token.ValidTo);
    }

    public (string refreshToken, DateTime expirationRefreshTokenTime) GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        
        var refreshToken = Convert.ToBase64String(randomNumber);
        
        return (refreshToken, DateTime.Now.AddDays(_jwtConfiguration.RefreshTokenLifetimeInDays));
    }
}