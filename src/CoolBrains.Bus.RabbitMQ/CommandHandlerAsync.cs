using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Commands;
using CoolBrains.Infrastructure.Events;
using CoolBrains.Infrastructure.Session;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace CoolBrains.Infrastructure.Bus.RabbitMQ
{
    public abstract class CommandHandlerAsync<T> : IConsumer<T>, ICommandHandlerAsync<T> where T : class, ICommand
    {
        private readonly UserContext _userContext;
        private IDomainEventProcessor _eventProcessor;
        private readonly IServiceProvider _serviceProvider;
        protected CommandHandlerAsync(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _userContext = serviceProvider.GetService<UserContext>();
        }
        public async Task Consume(ConsumeContext<T> context)
        {
            _userContext.Set(context.Message.UserContext);
            var response = await HandleAsync(context.Message);
            _eventProcessor = _serviceProvider.GetService<IDomainEventProcessor>();
            await _eventProcessor.Process(response.Events, context.Message);
        }

        public abstract Task<CommandResponseWithEvents> HandleAsync(T command);
    }
}
