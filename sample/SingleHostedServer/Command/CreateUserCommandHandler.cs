using System;
using System.Collections.Generic;
using System.Text;
using CoolBrains.Infrastructure.Commands;
using SingleHostedServer.Domain;

namespace SingleHostedServer.Command
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        public CommandResponse Handle(CreateUserCommand command)
        {
            var user = new User(command.UserName, command.Email);
            return new CommandResponse
            {
                Events = user.Events
            };
        }
    }
}
