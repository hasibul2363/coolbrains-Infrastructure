using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Bus;//.RabbitMQ;
using CoolBrains.Infrastructure.Commands;
using SingleHostedServer.Domain;

namespace SingleHostedServer.Command
{
    public class CreateUserCommandHandler : CommandHandlerAsync<CreateUserCommand>
    {
       
        public override async Task<CommandResponseWithEvents> HandleAsync(CreateUserCommand command)
        {
            Console.WriteLine("I am from CreateUserCommandHandler");
            var user = new User(command.UserName, command.Email, command.UserId);
            return new CommandResponseWithEvents
            {
                Events = user.Events
            };
        }

        public CreateUserCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
