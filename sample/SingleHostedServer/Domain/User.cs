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

        public User()
        {
            
        }
        public User(string userName, string email, Guid userId) : base(userId)
        {
            AddAndApplyEvent<User>(new UserCreated
            {
                AggregateRootId = userId,
                Email = email, 
                UserName = userName, 
            });
        }

        private void Apply(UserCreated @event)
        {
            this.Id = @event.AggregateRootId;
            this.Email = @event.Email;
            this.UserName = @event.UserName;
        }


        public void Update(string userName)
        {
            AddAndApplyEvent<User>(new UserUpdated
            {
                Email = Email,
                UserName = userName,
                AggregateRootId = Id
            });
        }

        private void Apply(UserUpdated @event)
        {
            this.UserName = @event.UserName;
        }
    }
}
