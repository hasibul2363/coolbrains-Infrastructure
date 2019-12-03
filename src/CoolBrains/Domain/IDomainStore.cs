using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoolBrains.Infrastructure.Domain
{
    public interface IDomainStore
    {
        void Save(Type aggregateType, Guid aggregateRootId, IEnumerable<IDomainEvent> events);
        Task SaveAsync(Type aggregateType, Guid aggregateRootId, IEnumerable<IDomainEvent> events);
        IEnumerable<IDomainEvent> GetEvents(Guid aggregateId);
        Task<IEnumerable<IDomainEvent>> GetEventsAsync(Guid aggregateId);
    }
}
