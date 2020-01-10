using System;
using Microsoft.AspNetCore.Http;

namespace CoolBrains.Infrastructure.Host.AspNetCore.Authentication
{
    public static class Keys
    {
        public static string Authorization = "Authorization";
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
    }
}
