using System;
using System.Collections.Generic;
using System.Text;

namespace CoolBrains.Infrastructure.Session
{
    public class UserContext
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public string[] Roles { get; set; }
    }
}
