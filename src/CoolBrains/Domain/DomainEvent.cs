using System;
using CoolBrains.Infrastructure.Events;

namespace CoolBrains.Infrastructure.Domain
{
    public class DomainEvent: Event, IDomainEvent
    {
        public Guid Id { get; set; }
        public Guid AggregateRootId { get; set; }
    }
}
