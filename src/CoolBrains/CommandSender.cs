using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commands;
using CoolBrains.Dependencies;
using CoolBrains.Infrastructure.Domain;
using CoolBrains.Infrastructure.Domain.Events;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains
{
    public class CommandSender : ICommandSender
    {
        private readonly IDomainStore _domainStore;
        private readonly IHandlerResolver _handlerResolver;
        private readonly UserContext _userContext;
        public CommandSender(IHandlerResolver handlerResolver, UserContext userContext, IDomainStore domainStore)
        {
            _handlerResolver = handlerResolver;
            _userContext = userContext;
            _domainStore = domainStore;
        }

        public async Task<CommandResponse> SendAsync(ICommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            if (command.UserContext == null)
                command.SetUserContext(_userContext);

            //TODO Automatic validation

            var handler = _handlerResolver.ResolveHandler(command, typeof(ICommandHandlerAsync<>));
            var handleMethod = handler.GetType().GetMethod("HandleAsync", new[] { command.GetType() });
            var response = await (Task<CommandResponse>)handleMethod.Invoke(handler, new object[] { command });

            if (response == null)
                return null;

            if (response.Events == null)
                return response;
            
            if (response.Events != null)
                foreach (var @event in (IEnumerable<IDomainEvent>)response.Events)
                {
                    if (@event.UserContext == null)
                        @event.SetUserContext(_userContext);
                }

            
            var events = (IEnumerable<IDomainEvent>) response.Events;
            var aggregateDetail = events.First();

            await _domainStore.SaveAsync(aggregateDetail.Source, aggregateDetail.AggregateRootId, events);

            if (command.PublishEvent)
            {
                foreach (var @event in response.Events)
                    await _eventPublisher.PublishAsync(@event);
            }

            return response;
        }
    }
}
