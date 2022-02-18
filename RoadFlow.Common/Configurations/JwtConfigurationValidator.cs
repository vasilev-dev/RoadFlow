using FluentValidation;

namespace RoadFlow.Common.Configurations;

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