using System;
using CoolBrains.Infrastructure.Events;

namespace CoolBrains.Infrastructure.Domain
{
    public class DomainEvent: Event, IDomainEvent
    {
        public DomainEvent()
        {
            Id = Guid.NewGuid();
        }
        public DomainEvent(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
        public Guid AggregateRootId { get; set; }
    }
}
