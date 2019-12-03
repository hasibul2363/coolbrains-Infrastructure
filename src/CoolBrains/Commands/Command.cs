using System;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Commands
{
    public abstract class Command : ICommand
    {
        public UserContext UserContext { get; private set; }
        public DateTime TimeStamp { get; set; } =  DateTime.UtcNow;
        public bool PublishEvent { get; set; } = true;
        public void SetUserContext(UserContext userContext) => UserContext = userContext;
    }
}
