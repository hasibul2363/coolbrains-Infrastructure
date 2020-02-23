using System;
using System.Collections.Generic;
using System.Text;

namespace CoolBrains.Infrastructure.Session
{
    public class UserContext
    {
        private readonly Guid _anonymousUserId = Guid.Parse("20c69484-b359-4a48-9155-d877198d5db4");

        public UserContext()
        {
            UserId = _anonymousUserId;
        }

        public UserContext(Guid userId, Guid tenantId)
        {
            UserId = userId;
            TenantId = tenantId;
        }
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public string[] Roles { get; set; } = new[] {"anonymous"};
        public Guid ClientId { get; set; }
        public string Audience { get; set; }
        public string TokenIssuer { get; set; }
        public string LanguageCode { get; set; }



        public void Set(UserContext context)
        {
            UserId = context.UserId;
            TenantId = context.TenantId;
            Roles = context.Roles;
            ClientId = context.ClientId;
            Audience = context.Audience;
            TokenIssuer = context.TokenIssuer;
            LanguageCode = context.LanguageCode;
        }

        public bool IsAnonymous() => UserId == _anonymousUserId;


    }
}
