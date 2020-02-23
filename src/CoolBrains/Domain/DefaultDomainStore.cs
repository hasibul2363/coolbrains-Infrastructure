using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoolBrains.Infrastructure.Domain
{
    public class DefaultDomainStore : IDomainStore
    {
        public void Save(Guid aggregateRootId, IEnumerable<IDomainEvent> events)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(Guid aggregateRootId, IEnumerable<IDomainEvent> events)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IDomainEvent> GetEvents(Guid aggregateId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IDomainEvent>> GetEventsAsync(Guid aggregateId)
        {
            throw new NotImplementedException();
        }
    }
}
