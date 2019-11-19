using System;

namespace CoolBrains.Infrastructure.Domain
{
    public interface IDomainEvent : IEvent
    {
        Guid Id { get; set; }
        Guid AggregateRootId { get; set; }
    }
}
