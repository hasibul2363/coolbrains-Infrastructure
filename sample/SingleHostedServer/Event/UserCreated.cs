using System;
using CoolBrains.Infrastructure.Domain;
using CoolBrains.Infrastructure.Events;

namespace SingleHostedServer.Event
{
    public class UserCreated : DomainEvent, IEvent
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
