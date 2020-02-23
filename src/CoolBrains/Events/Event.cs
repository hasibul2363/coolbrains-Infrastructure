using System;
using CoolBrains.Infrastructure.Bus;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Events
{
    public class InMemoryEvent : Message, IEvent
    {
        public string Source { get; set; }
    }

    public class Event : BusTopicMessage, IEvent
    {
        public string Source { get; set; }
    }
}
