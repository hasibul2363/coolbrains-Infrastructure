using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Bus;
using CoolBrains.Infrastructure.Dependencies;
using CoolBrains.Infrastructure.Session;
using ReflectionMagic;

namespace CoolBrains.Infrastructure.Events
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IBusMessageDispatcher _busMessageDispatcher;
        private UserContext _userContext;
        private IHandlerResolver _handlerResolver;

        public EventPublisher(IBusMessageDispatcher busMessageDispatcher, UserContext userContext, IHandlerResolver handlerResolver)
        {
            _busMessageDispatcher = busMessageDispatcher;
            _handlerResolver = handlerResolver;
            _userContext = userContext;
        }


        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event));

            _userContext = @event.UserContext;

            //TODO there is a probability to have multiple handlers
            var handler = _handlerResolver.ResolveHandler(@event, typeof(IEventHandlerAsync<>));

            //foreach (var handler in handlers)
            if (handler != null)
                await handler.AsDynamic().HandleAsync(@event);

            if (@event is IBusMessage message)
                await _busMessageDispatcher.DispatchAsync(message);
        }


        public async Task PublishAsync<TEvent>(IEnumerable<TEvent> events) where TEvent : IEvent
        {
            foreach (var @event in events)
            {
                await PublishAsync(@event);
            }
        }


    }
}
