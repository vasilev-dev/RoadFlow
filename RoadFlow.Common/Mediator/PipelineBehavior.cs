using FluentValidation;
using MediatR;

namespace RoadFlow.Common.Mediator;

public class PipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public PipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        await ValidateRequestAndThrow(request, cancellationToken);

        return await next();
    }

    private Task ValidateRequestAndThrow(TRequest request, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var failures = _validators
            .Select(async x => await x.ValidateAsync(context, cancellationToken))
            .Select(t => t.Result)
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .ToList();

        if (failures.Any())
            throw new ValidationException(failures);

        return Task.CompletedTask;
    }
}