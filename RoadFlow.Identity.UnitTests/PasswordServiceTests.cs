using FluentAssertions;
using RoadFlow.Identity.Core.Domains.User.Common.Password;
using Xunit;

namespace RoadFlow.Identity.UnitTests;

public class PasswordServiceTests
{
    private readonly PasswordService _passwordService = new();
    
    [Theory]
    [InlineData("Password1!")]
    [InlineData("1234")]
    [InlineData("пароль")]
    public void GeneratePasswordHashAndSalt_VerifyPassword_ShouldCreateAndVerifyPassword(string password)
    {
        var (passwordHash, passwordSalt) = _passwordService.GeneratePasswordHashAndSalt(password);

        var actual = _passwordService.VerifyPassword(password, passwordHash, passwordSalt);

        actual.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("Password1!", "Password1?")]
    [InlineData("1234", "123")]
    [InlineData("пароль", "парольь")]
    public void GeneratePasswordHashAndSalt_VerifyPassword_ShouldCreateAndNotVerifyWrongPassword(string password, string wrongPassword)
    {
        var (passwordHash, passwordSalt) = _passwordService.GeneratePasswordHashAndSalt(password);

        var actual = _passwordService.VerifyPassword(wrongPassword, passwordHash, passwordSalt);

        actual.Should().BeFalse();
    }
}