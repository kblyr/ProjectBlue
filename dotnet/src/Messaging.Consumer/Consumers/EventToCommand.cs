using JIL.Contracts;
using MapsterMapper;
using MassTransit;

namespace JIL.Consumers;

public abstract class EventToCommandConsumer<TEvent, TCommand> : IConsumer<TEvent>
    where TEvent : class, MediatR.INotification
    where TCommand : IRequest
{
    readonly MediatR.IMediator _mediator;
    readonly IMapper _mapper;

    protected EventToCommandConsumer(MediatR.IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<TEvent> context)
    {
        await _mediator.Send(_mapper.Map<TEvent, TCommand>(context.Message));
    }
}