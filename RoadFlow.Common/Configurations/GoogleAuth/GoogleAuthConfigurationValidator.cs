using FluentValidation;

namespace RoadFlow.Common.Configurations.GoogleAuth;

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