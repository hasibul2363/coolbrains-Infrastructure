using System.Threading.Tasks;
using CoolBrains.Infrastructure.Events;
using MassTransit;

namespace CoolBrains.Bus.RabbitMQ
{
    public abstract class EventHandlerAsync<T> :IConsumer<T>, IEventHandlerAsync<T> where T: class, IEvent
    {
        public Task Consume(ConsumeContext<T> context)
        {
            return this.HandleAsync(context.Message);
        }

        public abstract Task HandleAsync(T @event);
    }
}
