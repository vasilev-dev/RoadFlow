using FluentValidation;

namespace RoadFlow.Common.Configurations.Jwt;

public class JwtConfigurationValidator : AbstractValidator<JwtConfiguration>
{
    public JwtConfigurationValidator()
    {
        RuleFor(x => x.ValidAudience)
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.ValidIssuer)
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.IssuerSigningKey)
            .NotNull()
            .NotEmpty();
    }
}