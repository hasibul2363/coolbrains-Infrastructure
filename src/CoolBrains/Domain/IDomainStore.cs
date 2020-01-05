using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoolBrains.Infrastructure.Domain
{
    public interface IDomainStore
    {
        void Save(Guid aggregateRootId, IEnumerable<IDomainEvent> events);
        Task SaveAsync(Guid aggregateRootId, IEnumerable<IDomainEvent> events);
        IEnumerable<IDomainEvent> GetEvents(Guid aggregateId);
        Task<IEnumerable<IDomainEvent>> GetEventsAsync(Guid aggregateId);
    }
}
