using System;
using System.Collections.Generic;
using System.Text;
using SingleHostedServer.Domain;

namespace SingleHostedServer.Command
{
    public class CreateUserCommand : CoolBrains.Infrastructure.Commands.Command
    {
        public CreateUserCommand()
        {
            UserId = Guid.NewGuid();
        }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
