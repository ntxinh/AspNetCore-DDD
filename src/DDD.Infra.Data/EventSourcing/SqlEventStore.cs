using System.Text.Json;
using DDD.Domain.Core.Events;
using DDD.Infra.Data.Repository.EventSourcing;

namespace DDD.Infra.Data.EventSourcing
{
    public class SqlEventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public SqlEventStore(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public void Save<T>(T theEvent) where T : Event
        {
            var serializedData = JsonSerializer.Serialize(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                "YourUserName");

            _eventStoreRepository.Store(storedEvent);
        }
    }
}
