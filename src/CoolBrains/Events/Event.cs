using System;
using CoolBrains.Infrastructure.Bus;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Events
{
    public class Event : IEvent,IBusTopicMessage
    {
        public UserContext UserContext { get; private set; }
        public string Source { get; set; }
        public DateTime TimeStamp { get; set; }
        public void SetUserContext(UserContext userContext) => UserContext = userContext;
        public string TopicName { get; set; }
    }
}
