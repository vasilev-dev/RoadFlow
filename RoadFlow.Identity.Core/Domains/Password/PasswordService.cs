using System.Security.Cryptography;

namespace RoadFlow.Identity.Core.Domains.Password;

public class PasswordService : IPasswordService
{
    public (string passwordHash, string passwordSalt) GeneratePasswordHashAndSalt(string password)
    {
        var saltBytes = GenerateSalt();
        var passwordSalt = Convert.ToBase64String(saltBytes);

        var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
        var passwordHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
        
        return (passwordHash, passwordSalt);
    }

    public bool VerifyPassword(string password, string passwordHash, string passwordSalt)
    {
        var saltBytes = Convert.FromBase64String(passwordSalt);
        var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
        return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == passwordHash;
    }

    private static byte[] GenerateSalt()
    {
        return RandomNumberGenerator.GetBytes(128 / 8);
    }
}