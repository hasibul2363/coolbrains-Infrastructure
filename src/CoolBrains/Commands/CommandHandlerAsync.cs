using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Commands;
using CoolBrains.Infrastructure.Events;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Bus
{
    public abstract class CommandHandlerAsync<T> : ICommandHandlerAsync<T> where T : class, ICommand
    {
        private readonly IServiceProvider _serviceProvider;
        protected CommandHandlerAsync(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public abstract Task<CommandResponseWithEvents> HandleAsync(T command);
    }
}
