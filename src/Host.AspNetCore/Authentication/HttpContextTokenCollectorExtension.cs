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

            //TODO
            /*
            if (string.IsNullOrEmpty(token) && context.Request.Cookies != null)
            {
                var currentRequestOrigin = context.Request.Headers[Keys.Origin].ToString();
                if (string.IsNullOrWhiteSpace(currentRequestOrigin))
                    currentRequestOrigin = context.Request.Headers[Keys.Referer].ToString();

                if (!string.IsNullOrEmpty(currentRequestOrigin) && Uri.TryCreate(currentRequestOrigin, UriKind.Absolute, out var incomingUri))
                {
                    var siteSpecificTokenKey = incomingUri.Host;
                    token = (context.Request.Cookies.ContainsKey(siteSpecificTokenKey) && !string.IsNullOrWhiteSpace(context.Request.Cookies[siteSpecificTokenKey])) ? context.Request.Cookies[siteSpecificTokenKey] : string.Empty;
                }

                if (string.IsNullOrEmpty(token))
                    token = (context.Request.Cookies.ContainsKey(Keys.AccessToken) && !string.IsNullOrWhiteSpace(context.Request.Cookies[Keys.AccessToken])) ? context.Request.Cookies[Keys.AccessToken] : string.Empty;
            }
            */

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
