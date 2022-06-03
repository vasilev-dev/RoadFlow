using Bogus;
using FluentAssertions;
using RoadFlow.Identity.Core.Domains.User.Common;
using Xunit;

namespace RoadFlow.Identity.UnitTests;

public class UsernameValidatorTests
{
    private readonly UsernameValidator _usernameValidator = new ();
    private readonly Faker _faker = new ();

    [Fact]
    public void Validate_ShouldNotValidateUserWithLengthLess3()
    {
        var actual = _usernameValidator.Validate("Dm");

        actual.IsValid.Should().BeFalse();
        actual.Errors.Should().ContainSingle(x => x.ErrorCode == "MinimumLengthValidator");
    }
    
    [Fact]
    public void Validate_ShouldNotValidateUserWithLengthMore32()
    {
        var usernameWithBigLength = _faker.Random.String2(33);
        
        var actual = _usernameValidator.Validate(usernameWithBigLength);

        actual.IsValid.Should().BeFalse();
        actual.Errors.Should().ContainSingle(x => x.ErrorCode == "MaximumLengthValidator");
    }

    [Theory]
    [InlineData("_Dm")]
    [InlineData("1Dm")]
    [InlineData("異Dm")]
    [InlineData("$Dm")]
    public void Validate_ShouldNotValidateInvalidFirstSymbol(string username)
    {
        var actual = _usernameValidator.Validate(username);

        actual.IsValid.Should().BeFalse();
        actual.Errors.Should().ContainSingle(x => x.ErrorMessage == "Username must starts with latin letter");
    }
    
    [Theory]
    [InlineData("Dm$")]
    [InlineData("Dm異")]
    public void Validate_ShouldNotValidateInvalidSymbol(string username)
    {
        var actual = _usernameValidator.Validate(username);

        actual.IsValid.Should().BeFalse();
        actual.Errors.Should().ContainSingle(x => x.ErrorMessage == "Username must contains only latin letters, digits and underscore");
    }
    
    [Theory]
    [InlineData("D__")]
    [InlineData("Dm_")]
    [InlineData("Dm0")]
    [InlineData("D_0")]
    [InlineData("D0_")]
    [InlineData("DmitriyVasilev")]
    [InlineData("Dmitriy_Vasilev")]
    [InlineData("Dmitriy_Vasilev_0")]
    public void Validate_ShouldValidateCorrectUsername(string username)
    {
        var actual = _usernameValidator.Validate(username);

        actual.IsValid.Should().BeTrue();
    }
}