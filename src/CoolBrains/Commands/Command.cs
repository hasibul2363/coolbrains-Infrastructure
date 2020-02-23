using System;
using CoolBrains.Infrastructure.Bus;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Commands
{
    public abstract class InMemoryCommand : Message, ICommand
    {
        public bool PublishEvent { get; set; } = true;
    }

    public abstract class Command : BusQueueMessage, ICommand
    {
        public bool PublishEvent { get; set; } = true;
    }
}
