using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CoolBrains.Infrastructure.Security;
using CoolBrains.Infrastructure.Session;
using Microsoft.AspNetCore.Http;

namespace CoolBrains.Infrastructure.Host.AspNetCore.Authentication
{
    public abstract class WebSecurityContextInitializerBase
    {
        public void Initialize(HttpContext context, RequestInfo requestInfo, UserContext userContext)
        {
            string token = context.GetAuthorizationToken();
            var requestUri = GetUri(context.Request);
            requestInfo.HostName = requestUri.Host;
            requestInfo.RequestUri = requestUri;

            if (!string.IsNullOrEmpty(token))
            {
                var tokens = token.Split(' ');
                if (tokens.Length > 1)
                {
                    requestInfo.AuthorizationScheme = tokens[0].Trim();
                    requestInfo.AuthorizationParameter = tokens[1].Trim();
                }
                else
                {
                    requestInfo.AuthorizationParameter = tokens[0].Trim();
                    requestInfo.AuthorizationScheme = "bearer";
                }
            }

            var headers = new List<KeyValuePair<string, IEnumerable<string>>>();
            foreach (var requestHeader in context.Request.Headers.ToList())
                headers.Add(new KeyValuePair<string, IEnumerable<string>>(requestHeader.Key, requestHeader.Value.ToList()));
            requestInfo.Headers = headers;


            var authData = context.User.GetAuthData();
            userContext.UserId = authData.UserId;
            userContext.Audience = authData.Audience;
            userContext.ClientId = authData.ClientId;
            userContext.Roles = authData.Roles;
            userContext.TenantId = authData.TenantId;
            userContext.TokenIssuer = authData.TokenIssuer;
            
        }
        public static Uri GetUri(HttpRequest request)
        {
            var builder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host
            };
            if (request.Host.Port.HasValue)
            {
                builder.Port = request.Host.Port.Value;
            }
            builder.Path = request.Path;
            builder.Query = request.QueryString.ToUriComponent();
            return builder.Uri;
        }
    }
}
