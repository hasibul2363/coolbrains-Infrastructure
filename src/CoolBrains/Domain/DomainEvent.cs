using System;
using CoolBrains.Infrastructure.Events;

namespace CoolBrains.Infrastructure.Domain
{
    public abstract class InMemoryDomainEvent : InMemoryEvent, IDomainEvent
    {
        protected InMemoryDomainEvent()
        {
            Id = Guid.NewGuid();
        }
        private InMemoryDomainEvent(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
        public Guid AggregateRootId { get; set; }
    }

    public abstract class DomainEvent : Event, IDomainEvent
    {
        protected DomainEvent()
        {
            Id = Guid.NewGuid();
        }
        private DomainEvent(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
        public Guid AggregateRootId { get; set; }
    }
}
