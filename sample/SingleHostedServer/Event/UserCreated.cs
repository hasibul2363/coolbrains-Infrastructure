using System;
using CoolBrains.Infrastructure.Domain;

namespace SingleHostedServer.Event
{
    public class UserCreated : DomainEvent
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
