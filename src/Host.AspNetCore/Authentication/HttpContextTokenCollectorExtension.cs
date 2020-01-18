using System;
using CoolBrains.Infrastructure.Session;
using Microsoft.AspNetCore.Http;

namespace CoolBrains.Infrastructure.Host.AspNetCore.Authentication
{
    public static class Keys
    {
        public static string Authorization = "Authorization";
        public static string ClientId = "c-clientId";
        public static string TenantId = "c-tenantId";
    }

    public static class HttpContextTokenCollectorExtension
    {
        public static string GetAuthorizationToken(this HttpContext context)
        {
            string token = context.Request.Query[Keys.Authorization].ToString();

            if (string.IsNullOrEmpty(token))
                token = context.Request.Headers[Keys.Authorization];

            return token;
        }

        public static void SetClientIdAndTenantIdToUserContext(this HttpContext context, UserContext userContext)
        {
            string clientId = context.Request.Query[Keys.ClientId].ToString();
            string tenantId = context.Request.Query[Keys.TenantId].ToString();

            if (string.IsNullOrEmpty(tenantId))
            {
                clientId = context.Request.Headers[Keys.ClientId];
                tenantId = context.Request.Headers[Keys.TenantId];
            }

            if (!string.IsNullOrEmpty(clientId))
                userContext.ClientId = Guid.Parse(clientId); 
            
            if (!string.IsNullOrEmpty(tenantId))
                userContext.TenantId = Guid.Parse(tenantId);
        }
    }
}
