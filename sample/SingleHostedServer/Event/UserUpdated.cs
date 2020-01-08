using System;
using System.Collections.Generic;
using System.Text;
using CoolBrains.Infrastructure.Bus;
using CoolBrains.Infrastructure.Domain;
using CoolBrains.Infrastructure.Events;

namespace SingleHostedServer.Event
{
    public class UserUpdated : DomainEvent, IEvent, IBusTopicMessage
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string TopicName { get; set; }
    }
}
