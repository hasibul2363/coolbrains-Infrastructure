using System;

namespace CoolBrains.Infrastructure.Domain.Events
{
    public interface IDomainEvent : IEvent
    {
        Guid Id { get; set; }
        Guid AggregateRootId { get; set; }
    }
}
