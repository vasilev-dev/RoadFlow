using MongoDB.Bson.Serialization.Attributes;

namespace RoadFlow.Identity.Core.Domains.User;

public class User
{
    [BsonId]
    public string Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpirationRefreshTokenTime { get; set; }
}