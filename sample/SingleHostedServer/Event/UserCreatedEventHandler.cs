using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Events;

namespace SingleHostedServer.Event
{
    public class UserCreatedEventHandler : IEventHandlerAsync<UserCreated>
    {
        public async Task HandleAsync(UserCreated @event)
        {
            Console.WriteLine("I am from event handler with user Name {}");
        }
    }
}
