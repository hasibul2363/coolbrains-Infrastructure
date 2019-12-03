using System;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Events
{
    public class Event : IEvent
    {
        public UserContext UserContext { get; private set; }
        public Type Source { get; set; }
        public DateTime TimeStamp { get; set; }
        public void SetUserContext(UserContext userContext) => UserContext = userContext;
    }
}
