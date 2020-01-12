using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.OAuth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CoolBrains.Infrastructure.Host.AspNetCore.Authentication
{
    public static class AuthenticationHelper
    {
        private static TokenConfig _tokenConfig = new TokenConfig();
        public static void AddJwtBearerAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration, Action<JwtBearerOptions> configureOptions = null, JwtBearerEvents bearerEvents = null)
        {
            configuration.GetSection("TokenConfig").Bind(_tokenConfig);
            
            if (configureOptions == null)
            {
                configureOptions = options =>
                {


                    
                    options.Audience = "security.coolbrains.co";
                    options.ClaimsIssuer = "security.coolbrains.co";
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidAudience = "security.coolbrains.co",
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = false,
                        ValidIssuer = "security.coolbrains.co",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("2363-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv"))


                    };

                    //if (bearerEvents != null)
                    //    options.Events = bearerEvents;
                    //else
                    //    options.Events = new JwtBearerEvents
                    //    {
                    //        OnMessageReceived = OnMessageReceived,
                    //        OnTokenValidated = TokenValidated
                    //    };
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
            
            var token = messageReceivedContext.HttpContext.GetAuthorizationToken();
            if (!string.IsNullOrEmpty(token))
            {
                var tokens = token.Split(' ');
                messageReceivedContext.Token = tokens.Count() == 2 ? tokens[1] : tokens[0];
            }
            return Task.CompletedTask;
        }
    }
}
