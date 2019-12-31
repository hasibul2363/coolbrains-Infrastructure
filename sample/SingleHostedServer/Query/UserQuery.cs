using System;
using System.Collections.Generic;
using System.Text;
using CoolBrains.Infrastructure.Queries;
using CoolBrains.Infrastructure.Session;
using SingleHostedServer.Event.User;

namespace SingleHostedServer.Query
{
    public class UserQuery : Query<List<UserInfo>>
    {
        public string SearchText { get; set; }
    }
}
