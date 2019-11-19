using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CoolBrains.Infrastructure.Domain
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
        int Version { get; }
        ReadOnlyCollection<IDomainEvent> Events { get; }
        void LoadsFromHistory(IEnumerable<IDomainEvent> events);
    }
}
