using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Domain;
using CoolBrains.Infrastructure.Store.Abstraction;
using MongoDB.Bson.IO;

namespace CoolBrains.Infrastructure.Store.Mongo
{
    public class DomainStore : IDomainStore
    {
        private readonly IRepository _repository;

        public DomainStore(IRepository repository)
        {
            _repository = repository;
        }

        public void Save(Type aggregateType, Guid aggregateRootId, IEnumerable<IDomainEvent> events)
        {
            if (events == null)
                return;

            var eventDocs = new List<EventDocument>();
            var currentVersion = _repository.GetItems<EventDocument>(p => p.AggregateId == aggregateRootId).Count();
            events.ToList().ForEach(e => eventDocs.Add(Build(e, ++currentVersion)));
            _repository.Save(eventDocs);
        }



        public Task SaveAsync(Type aggregateType, Guid aggregateRootId, IEnumerable<IDomainEvent> events)
        {
            if (events == null)
                return Task.CompletedTask;

            var eventDocs = new List<EventDocument>();
            var currentVersion = _repository.GetItems<EventDocument>(p => p.AggregateId == aggregateRootId).Count();
            events.ToList().ForEach(e => Build(e, ++currentVersion));
            return _repository.SaveAsync(eventDocs);
        }

        public IEnumerable<IDomainEvent> GetEvents(Guid aggregateId)
        {
            var domainEvents = new List<IDomainEvent>();
            var events = _repository.GetItems<EventDocument>(p => p.AggregateId == aggregateId);
            foreach (var eventDocument in events)
            {
                var domainEvent = Newtonsoft.Json.JsonConvert.DeserializeObject(eventDocument.Data, Type.GetType(eventDocument.Type));
                domainEvents.Add((IDomainEvent) domainEvent);
            }

            return domainEvents;
        }

        public async Task<IEnumerable<IDomainEvent>> GetEventsAsync(Guid aggregateId)
        {
            var domainEvents = new List<IDomainEvent>();
            var events = _repository.GetItems<EventDocument>(p => p.AggregateId == aggregateId);
            foreach (var eventDocument in events)
            {
                var domainEvent = Newtonsoft.Json.JsonConvert.DeserializeObject(eventDocument.Data, Type.GetType(eventDocument.Type));
                domainEvents.Add((IDomainEvent)domainEvent);
            }

            return domainEvents;
        }


        private EventDocument Build(IDomainEvent @event, int sequence)
        {
            return new EventDocument
            {
                Id = @event.Id,
                AggregateId = @event.AggregateRootId,
                AggregateType = @event.Source.Name,
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(@event),
                Type = @event.GetType().Name,
                Sequence = sequence,
                UserId = @event.UserContext.UserId,
                TimeStamp = @event.TimeStamp
            };
        }
    }
}
