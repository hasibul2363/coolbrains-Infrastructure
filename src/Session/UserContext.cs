using System;
using System.Collections.Generic;
using System.Text;

namespace CoolBrains.Infrastructure.Session
{
    public class UserContext
    {
        public UserContext()
        {
            
        }

        public UserContext(Guid userId, Guid tenantId)
        {
            UserId = userId;
            TenantId = tenantId;
        }
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public string[] Roles { get; set; }
        public Guid ClientId { get; set; }
        public string[] Audiences { get; set; }
        public string TokenIssuer { get; set; }
    }
}
