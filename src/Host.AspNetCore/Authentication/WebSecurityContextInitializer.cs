using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Session;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CoolBrains.Infrastructure.Host.AspNetCore.Authentication
{
    public class WebSecurityContextInitializer : WebSecurityContextInitializerBase
    {

        private readonly RequestDelegate _next;
        public WebSecurityContextInitializer(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context, RequestInfo requestInfo, UserContext userContext)
        {
            base.Initialize(context, requestInfo,userContext);
            return this._next(context);
        }
    }

    public static class WebSecurityContextInitializerMiddlewareExtension
    {
        public static IApplicationBuilder UseWebSecurityContextInitializer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WebSecurityContextInitializer>();
        }
    }
}
