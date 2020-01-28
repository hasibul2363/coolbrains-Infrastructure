using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Dependencies;
using CoolBrains.Infrastructure.Domain;
using CoolBrains.Infrastructure.Events;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Commands
{
    public class CommandSender : ICommandSender
    {

        private readonly IDomainEventProcessor _eventProcessor;
        private readonly IHandlerResolver _handlerResolver;
        private UserContext _userContext;
        public CommandSender(IHandlerResolver handlerResolver, UserContext userContext, IDomainEventProcessor eventProcessor)
        {
            _handlerResolver = handlerResolver;
            _userContext = userContext;
            _eventProcessor = eventProcessor;
        }



        public async Task<CommandResponse> SendAsync(ICommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            _userContext = command.UserContext;

            var handler = _handlerResolver.ResolveHandler(command, typeof(ICommandHandlerAsync<>));
            var handleMethod = handler.GetType().GetMethod("HandleAsync", new[] { command.GetType() });
            var response = await (Task<CommandResponseWithEvents>)handleMethod.Invoke(handler, new object[] { command });

            if (response == null)
                return null;

            if (response.Events == null)
                return new CommandResponse(response.ValidationResult, response.ValidationResult);

            await _eventProcessor.Process(response.Events, command);

            return new CommandResponse(response.ValidationResult, response.ValidationResult);
        }



        public CommandResponse Send(ICommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            if (command.UserContext == null)
                command.SetUserContext(_userContext);
            else
                _userContext = command.UserContext;

            //TODO Automatic validation

            var handler = _handlerResolver.ResolveHandler(command, typeof(ICommandHandler<>));
            var handleMethod = handler.GetType().GetMethod("Handle", new[] { command.GetType() });
            var response = (CommandResponseWithEvents)handleMethod.Invoke(handler, new object[] { command });

            if (response == null)
                return null;

            if (response.Events == null)
                return new CommandResponse(response.ValidationResult, response.Result);

            _eventProcessor.Process(response.Events, command).GetAwaiter().GetResult();

            return new CommandResponse(response.ValidationResult, response.ValidationResult);
        }
    }
}
