using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Commands;
using CoolBrains.Infrastructure.Domain;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Events
{
    public class DomainEventProcessor : IDomainEventProcessor
    {
        private readonly IDomainStore _domainStore;
        private readonly IEventPublisher _eventPublisher;
        private readonly UserContext _userContext;

        public DomainEventProcessor(IDomainStore domainStore, IEventPublisher eventPublisher, UserContext userContext)
        {
            _domainStore = domainStore;
            _eventPublisher = eventPublisher;
            _userContext = userContext;
        }

        public async Task Process(IEnumerable<IEvent> events, ICommand command)
        {
            try
            {
                if (events == null || !events.Any()) return;
                var domainEvents = (IEnumerable<IDomainEvent>)events;
                await Store(domainEvents);
                await PublishToBus(domainEvents, command);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public Task Store(IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                if (@event.UserContext == null)
                    @event.SetUserContext(_userContext);
            }
            var aggregateDetail = events.First();
            return _domainStore.SaveAsync(aggregateDetail.AggregateRootId, events);
        }

        public async Task PublishToBus(IEnumerable<IDomainEvent> events, ICommand command)
        {
            if (!command.PublishEvent) return;
            foreach (var @event in events)
                await _eventPublisher.PublishAsync(@event);
        }

    }
}
