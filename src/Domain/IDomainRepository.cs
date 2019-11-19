using System;
using System.Threading.Tasks;

namespace CoolBrains.Infrastructure.Domain
{
    public interface IDomainRepository<T> where T : IAggregateRoot
    {
        Task SaveAsync(T aggregate);
        void Save(T aggregate);
        Task<T> GetByIdAsync(Guid id);
        T GetById(Guid id);       
    }
}