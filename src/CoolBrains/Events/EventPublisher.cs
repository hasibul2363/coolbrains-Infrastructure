using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Bus;
using CoolBrains.Infrastructure.Dependencies;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Events
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IResolver _resolver;
        private readonly IBusMessageDispatcher _busMessageDispatcher;
        private UserContext _userContext;

        public EventPublisher(IResolver resolver, IBusMessageDispatcher busMessageDispatcher, UserContext userContext)
        {
            _resolver = resolver;
            _busMessageDispatcher = busMessageDispatcher;
        }

  
        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event));


            if (@event.UserContext == null)
                @event.SetUserContext(_userContext);
            else
                _userContext = @event.UserContext;
            
            var handlers = _resolver.ResolveAll<IEventHandlerAsync<TEvent>>();

            foreach (var handler in handlers)
                await handler.HandleAsync(@event);

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
