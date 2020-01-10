using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.OAuth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CoolBrains.Infrastructure.AspNet.Authentication
{
    public static class AuthenticationHelper
    {
        private static TokenConfig _tokenConfig;
        public static void AddJwtBearerAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration, Action<JwtBearerOptions> configureOptions = null, JwtBearerEvents bearerEvents = null)
        {
            configuration.GetSection("TokenConfig").Bind(_tokenConfig);
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenConfig.TokenSigningKey));
            if (configureOptions == null)
            {
                configureOptions = options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidAudience = "*",
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuers = _tokenConfig.TokenIssuers,
                        IssuerSigningKeys = new[] { signingKey }
                    };

                    if (bearerEvents != null)
                        options.Events = bearerEvents;
                    else
                        options.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = OnMessageReceived,
                            OnTokenValidated = TokenValidated
                        };
                };
            }

            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(configureOptions);
        }


        private static Task TokenValidated(TokenValidatedContext tokenValidatedContext)
        {
            return Task.CompletedTask;
        }
        private static Task OnMessageReceived(MessageReceivedContext messageReceivedContext)
        {
            /*
            var token = messageReceivedContext.HttpContext.GetAuthorizationToken();
            if (!string.IsNullOrEmpty(token))
            {
                var tokens = token.Split(' ');
                messageReceivedContext.Token = tokens.Count() == 2 ? tokens[1] : tokens[0];
            }*/
            return Task.CompletedTask;
        }
    }
}
