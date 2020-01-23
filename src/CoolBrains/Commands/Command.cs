using System;
using CoolBrains.Infrastructure.Bus;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Commands
{
    public abstract class Command : ICommand,IBusQueueMessage
    {
        public UserContext UserContext { get; private set; }
        public DateTime TimeStamp { get; set; } =  DateTime.UtcNow;
        public bool PublishEvent { get; set; } = true;
        public void SetUserContext(UserContext userContext) => UserContext = userContext;
        public string QueueName { get; set; } = "CoolBrains.Infrastructure.Commands";
    }
}
