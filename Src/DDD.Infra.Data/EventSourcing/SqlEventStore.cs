using System.Text.Json;

using DDD.Domain.Core.Events;
using DDD.Domain.Interfaces;
using DDD.Infra.Data.Repository.EventSourcing;

namespace DDD.Infra.Data.EventSourcing;

public class SqlEventStore(IEventStoreRepository eventStoreRepository, IUser user) : IEventStore
{
    private readonly IEventStoreRepository _eventStoreRepository = eventStoreRepository;
    private readonly IUser _user = user;

    public void Save<T>(T theEvent)
        where T : Event
    {
        var serializedData = JsonSerializer.Serialize(theEvent);

        var storedEvent = new StoredEvent(
            theEvent,
            serializedData,
            _user.Name);

        _eventStoreRepository.Store(storedEvent);
    }
}
