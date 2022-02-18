using FluentValidation;

namespace RoadFlow.Common.Configurations;

public class MongoDbConfigurationValidator : AbstractValidator<MongoDbConfiguration>
{
    public MongoDbConfigurationValidator()
    {
        RuleFor(x => x.ConnectionString)
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.DatabaseName)
            .NotNull()
            .NotEmpty();
    }
}