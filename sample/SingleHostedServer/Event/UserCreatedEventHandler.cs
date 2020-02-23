using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Bus.RabbitMQ;
using CoolBrains.Infrastructure.Session;
using CoolBrains.Infrastructure.Store.Abstraction;
using SingleHostedServer.Event.User;

namespace SingleHostedServer.Event
{
    public class UserCreatedEventHandler : EventHandlerAsync<UserCreated> //IEventHandlerAsync<UserCreated>
    {
        private readonly IRepository _repository;

        private UserContext _userContext;
        public UserCreatedEventHandler(IRepository repository, UserContext userContext, IServiceProvider serviceProvider):base(serviceProvider)
        {
            _userContext = userContext;
            _repository = repository;
        }
        public override Task HandleAsync(UserCreated @event)
        {
            Console.WriteLine($"here is tenant id {_userContext.TenantId}");
            Console.WriteLine($"I am from {@event.GetType()} event handler");
            return _repository.SaveAsync(new UserInfo
            {
                Id = @event.AggregateRootId, UserName = @event.UserName, Email = @event.Email
            });
            
        }
    }
}
