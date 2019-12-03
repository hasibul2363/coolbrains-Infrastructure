using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoolBrains.Infrastructure.Domain
{
    public class DomainRepository<T> : IDomainRepository<T> where T : IAggregateRoot
    {
        public Task SaveAsync(T aggregate)
        {
            throw new NotImplementedException();
        }

        public void Save(T aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public T GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
