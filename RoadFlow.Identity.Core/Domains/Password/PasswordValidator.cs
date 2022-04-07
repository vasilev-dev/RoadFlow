using System.Text.RegularExpressions;
using FluentValidation;

namespace RoadFlow.Identity.Core.Domains.Password;

public class PasswordValidator : AbstractValidator<string>
{
    /*
    * minimum 8 characters
    * maximum 32 characters
    * at least one uppercase letter
    * one lowercase letter
    * one digit
    * one special character
    */
    private readonly Regex _regex = new("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,32}$");
    
    public PasswordValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .Matches(_regex);
    }
}