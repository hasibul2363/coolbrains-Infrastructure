using CoolBrains.Infrastructure.Commands;
using CoolBrains.Infrastructure.Events;
using CoolBrains.Infrastructure.Session;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CoolBrains.Infrastructure.Bus.RabbitMQ
{
    public abstract class EventHandlerAsync<T> : IConsumer<T>, IEventHandlerAsync<T> where T : class, IEvent
    {
        private UserContext _userContext;
        protected EventHandlerAsync(IServiceProvider serviceProvider)
        {
            _userContext = serviceProvider.GetService<UserContext>();
        }
        public Task Consume(ConsumeContext<T> context)
        {
            _userContext.Set(context.Message.UserContext);
            return this.HandleAsync(context.Message);
        }

        public abstract Task HandleAsync(T @event);
    }


    //public abstract class CommandHandlerAsync<T> : IConsumer<T>, ICommandHandlerAsync<T> where T : class, ICommand
    //{
        
    //}
}
