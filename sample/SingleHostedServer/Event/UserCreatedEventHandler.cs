using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Events;
using CoolBrains.Infrastructure.Store.Abstraction;
using SingleHostedServer.Event.User;

namespace SingleHostedServer.Event
{
    public class UserCreatedEventHandler : IEventHandlerAsync<UserCreated>
    {
        private readonly IRepository _repository;

        public UserCreatedEventHandler(IRepository repository)
        {
            _repository = repository;
        }
        public Task HandleAsync(UserCreated @event)
        {
            Console.WriteLine($"I am from {@event.GetType()} event handler");
            return _repository.SaveAsync(new UserInfo
            {
                Id = @event.AggregateRootId, UserName = @event.UserName, Email = @event.Email
            });
            
        }
    }
}
