using System;
using System.Collections.Generic;
using System.Text;

namespace SingleHostedServer.Command
{
    public class UserUpdateCommand : CoolBrains.Infrastructure.Commands.Command
    {
        public string UserName { get; set; }
        public Guid Id { get; set; }

    }
}
