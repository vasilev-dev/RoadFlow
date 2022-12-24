using MediatR;
using MediatR.Pipeline;

namespace RoadFlow.Common.MediatR;

public class EventSenderPostProcessor<TRequest, TResponse>: IRequestPostProcessor<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}