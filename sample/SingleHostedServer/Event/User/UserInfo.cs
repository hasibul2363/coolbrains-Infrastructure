using System;
using System.Collections.Generic;
using System.Text;

namespace SingleHostedServer.Event.User
{
    public class UserInfo
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
