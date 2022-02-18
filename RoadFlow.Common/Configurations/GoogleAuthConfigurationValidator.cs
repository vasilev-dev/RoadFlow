using FluentValidation;

namespace RoadFlow.Common.Configurations;

public class GoogleAuthConfigurationValidator : AbstractValidator<GoogleAuthConfiguration>
{
    public GoogleAuthConfigurationValidator()
    {
        RuleFor(x => x.ClientId)
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.ClientSecret)
            .NotNull()
            .NotEmpty();
    }
}