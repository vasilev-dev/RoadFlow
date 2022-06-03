namespace RoadFlow.Identity.Core.Domains.User.Common.Password;

public interface IPasswordService
{
    (string passwordHash, string passwordSalt) GeneratePasswordHashAndSalt(string password);
    bool VerifyPassword(string password, string passwordHash, string passwordSalt);
}