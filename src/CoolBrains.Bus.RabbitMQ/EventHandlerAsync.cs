using System.Threading.Tasks;
using CoolBrains.Infrastructure.Events;
using CoolBrains.Infrastructure.Session;
using MassTransit;

namespace CoolBrains.Infrastructure.Bus.RabbitMQ
{
    public abstract class EventHandlerAsync<T> : IConsumer<T>, IEventHandlerAsync<T> where T : class, IEvent
    {
        private readonly UserContext _userContext;
        protected EventHandlerAsync(UserContext userContext)
        {
            _userContext = userContext;
        }
        public Task Consume(ConsumeContext<T> context)
        {
            _userContext.Set(context.Message.UserContext);
            return this.HandleAsync(context.Message);
        }

        public abstract Task HandleAsync(T @event);
    }
}
