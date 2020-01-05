using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Store.Abstraction;

namespace CoolBrains.Infrastructure.Domain
{
    public class DomainRepositoryWithEs<T> : IDomainRepository<T> where T : IAggregateRoot
    {
        private readonly IDomainStore _domainStore;

        public DomainRepositoryWithEs(IDomainStore domainStore)
        {
            _domainStore = domainStore;
        }

        public async Task SaveAsync(T aggregate)
        {
            await _domainStore.SaveAsync(aggregate.Id, aggregate.Events);
        }

        public void Save(T aggregate)
        {
            _domainStore.Save(aggregate.Id, aggregate.Events);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var events = await _domainStore.GetEventsAsync(id);
            var domainEvents = events as IDomainEvent[] ?? events.ToArray();
            if (!domainEvents.Any())
            {
                return default;
            }

            var aggregate = Activator.CreateInstance<T>();
            aggregate.LoadsFromHistory(domainEvents);
            return aggregate;
        }

        public T GetById(Guid id)
        {
            var events = _domainStore.GetEvents(id);
            var domainEvents = events as IDomainEvent[] ?? events.ToArray();
            if (!domainEvents.Any())
            {
                return default;
            }

            var aggregate = Activator.CreateInstance<T>();
            aggregate.LoadsFromHistory(domainEvents);
            return aggregate;
        }
    }
}
