using System;
using System.Collections.Generic;
using System.Text;

namespace SingleHostedServer.Command
{
    public class CreateUserCommand : CoolBrains.Infrastructure.Commands.Command
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
