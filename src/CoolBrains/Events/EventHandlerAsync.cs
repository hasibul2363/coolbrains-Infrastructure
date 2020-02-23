using CoolBrains.Infrastructure.Events;
using System;
using System.Threading.Tasks;

namespace CoolBrains.Infrastructure.Bus
{
    public abstract class EventHandlerAsync<T> : IEventHandlerAsync<T> where T : class, IEvent
    {
        protected EventHandlerAsync(IServiceProvider serviceProvider)
        {

        }

        public abstract Task HandleAsync(T @event);
    }
}
