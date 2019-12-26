using System;
using System.Collections.Generic;
using System.Text;
using CoolBrains.Infrastructure.Domain;
using SingleHostedServer.Event;

namespace SingleHostedServer.Domain
{
    public class User: AggregateRoot
    {
        public string UserName { get; private set; }
        public string Email { get; private set; }

        public User(string userName, string email, Guid userId) : base(userId)
        {
            AddAndApplyEvent<User>(new UserCreated
            {
                Email = email, 
                UserName = userName, 
            });
        }

        private void Apply(UserCreated @event)
        {
            this.Email = @event.Email;
            this.UserName = @event.UserName;
        }

    }
}
