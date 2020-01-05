using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Commands;
using SingleHostedServer.Domain;

namespace SingleHostedServer.Command
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        public CommandResponseWithEvents Handle(CreateUserCommand command)
        {
            var user = new User(command.UserName, command.Email, command.UserId);
            return new CommandResponseWithEvents
            {
                Events = user.Events
            };
        }

    }
}
