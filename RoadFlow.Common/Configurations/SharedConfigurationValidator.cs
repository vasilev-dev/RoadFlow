using FluentValidation;

namespace RoadFlow.Common.Configurations;

public class SharedConfigurationValidator : AbstractValidator<SharedConfiguration>
{
    public SharedConfigurationValidator()
    {
        RuleFor(x => x.JwtConfiguration)
            .SetValidator(new JwtConfigurationValidator());

        RuleFor(x => x.GoogleAuthConfiguration)
            .SetValidator(new GoogleAuthConfigurationValidator());

        RuleFor(x => x.MongoDbConfiguration)
            .SetValidator(new MongoDbConfigurationValidator());

        RuleFor(x => x.AllowedClientOrigins)
            .NotNull();
    }
}