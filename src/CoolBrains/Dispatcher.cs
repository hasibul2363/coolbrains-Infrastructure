using System.Threading.Tasks;
using CoolBrains.Infrastructure.Bus;
using CoolBrains.Infrastructure.Commands;
using CoolBrains.Infrastructure.Events;
using CoolBrains.Infrastructure.Queries;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure
{

    public class Dispatcher : IDispatcher
    {
        private readonly ICommandSender _commandSender;
        private readonly IEventPublisher _eventPublisher;
        private readonly IQueryProcessor _queryProcessor;
        private readonly IBusMessageDispatcher _busMessageDispatcher;
        private UserContext _userContext;

        public Dispatcher(ICommandSender domainCommandSender,
            IEventPublisher eventPublisher,
            IQueryProcessor queryProcessor,
            IBusMessageDispatcher busMessageDispatcher, UserContext userContext)
        {
            _commandSender = domainCommandSender;
            _eventPublisher = eventPublisher;
            _queryProcessor = queryProcessor;
            _busMessageDispatcher = busMessageDispatcher;
            _userContext = userContext;
        }

        public Task SendBusMessageAsync<TMessage>(TMessage message) where TMessage : IMessage
        {
            SetUserContextWithMessage(message);
            return _busMessageDispatcher.DispatchAsync(message);
        }

        public Task PublishBusMessageAsync<TMessage>(TMessage message) where TMessage : IMessage
        {
            SetUserContextWithMessage(message);
            return _busMessageDispatcher.DispatchAsync(message);
        }

        public Task<CommandResponse> SendAsync(ICommand command)
        {
            SetUserContextWithMessage(command);
            return _commandSender.SendAsync(command);
        }

        public Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            SetUserContextWithMessage(@event);
            return _eventPublisher.PublishAsync(@event);
        }

        public Task<TResult> GetResultAsync<TResult>(IQuery<TResult> query)
        {
            SetUserContextWithMessage(query);
            return _queryProcessor.ProcessAsync(query);
        }

        public CommandResponse Send(ICommand command)
        {
            SetUserContextWithMessage(command);
            return _commandSender.Send(command);
        }

        public CommandResponse Send<TResult>(ICommand command)
        {
            SetUserContextWithMessage(command);
            return _commandSender.Send(command);
        }

        public TResult GetResult<TResult>(IQuery<TResult> query)
        {
            SetUserContextWithMessage(query);
            return _queryProcessor.Process(query);
        }


        private void SetUserContextWithMessage(ISecurityInfo message)
        {
            message.SetUserContext(_userContext);
        }
    }
}