using System.Threading.Tasks;

using DDD.Domain.Core.Bus;
using DDD.Domain.Core.Commands;
using DDD.Domain.Core.Events;

using MediatR;

namespace DDD.Infra.CrossCutting.Bus;

public sealed class InMemoryBus(IEventStore eventStore, IMediator mediator) : IMediatorHandler
{
    private readonly IMediator _mediator = mediator;
    private readonly IEventStore _eventStore = eventStore;

    public Task SendCommand<T>(T command)
        where T : Command => _mediator.Send(command);

    public Task RaiseEvent<T>(T @event)
        where T : Event
    {
        if (!@event.MessageType.Equals("DomainNotification"))
        {
            _eventStore?.Save(@event);
        }

        return _mediator.Publish(@event);
    }
}
