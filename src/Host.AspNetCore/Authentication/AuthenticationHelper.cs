using System;
using System.Text;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Bus;
using CoolBrains.Infrastructure.Extensions;
using CoolBrains.Infrastructure.OAuth;
using CoolBrains.Infrastructure.Session;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CoolBrains.Infrastructure.Host.AspNetCore.Authentication
{
    public static class AuthenticationHelper
    {
        private static void AddJwtBearerAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration, Action<JwtBearerOptions> configureOptions = null, JwtBearerEvents bearerEvents = null)
        {
            var tokenConfig = new TokenConfig();
            configuration.GetSection("TokenConfig").Bind(tokenConfig);
            
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfig.TokenSigningKey));
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
                        ValidIssuers = tokenConfig.TokenIssuers,
                        IssuerSigningKey = signingKey
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
        public static ICoolBrainsServiceBuilder AddAuth(this ICoolBrainsServiceBuilder builder, IConfiguration configuration, Action<JwtBearerOptions> configureOptions = null, JwtBearerEvents bearerEvents = null)
        {
            builder.Services.AddScoped<UserContext>();
            builder.Services.AddScoped<RequestInfo>();
            builder.Services.AddTransient<IOauthAccessTokenGenerator, OauthAccessTokenGenerator>();
            builder.Services.Configure<TokenConfig>(configuration.GetSection("TokenConfig"));
            builder.Services.AddJwtBearerAuthentication(configuration);
            return builder;
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
