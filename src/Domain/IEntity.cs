using System;

namespace CoolBrains.Infrastructure.Domain
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}
