using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoolBrains.Infrastructure.Events
{
    public interface IEventPublisher
    {
        Task PublishAsync<TEvent>(TEvent @event)
            where TEvent : IEvent;
        Task PublishAsync<TEvent>(IEnumerable<TEvent> events)
            where TEvent : IEvent;

    }
}
