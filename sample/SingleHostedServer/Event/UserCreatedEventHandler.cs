﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoolBrains.Bus.RabbitMQ;
using CoolBrains.Infrastructure.Events;
using CoolBrains.Infrastructure.Store.Abstraction;
using MassTransit;
using SingleHostedServer.Event.User;

namespace SingleHostedServer.Event
{
    public class UserCreatedEventHandler : EventHandlerAsync<UserCreated> //IEventHandlerAsync<UserCreated>
    {
        private readonly IRepository _repository;

        public UserCreatedEventHandler(IRepository repository)
        {
            _repository = repository;
        }
        public override Task HandleAsync(UserCreated @event)
        {
            Console.WriteLine($"I am from {@event.GetType()} event handler");
            return _repository.SaveAsync(new UserInfo
            {
                Id = @event.AggregateRootId, UserName = @event.UserName, Email = @event.Email
            });
            
        }


        //public async Task Consume(ConsumeContext<UserCreated> context)
        //{
        //    Console.WriteLine($"I am from  event handler");
        //}
    }
}
