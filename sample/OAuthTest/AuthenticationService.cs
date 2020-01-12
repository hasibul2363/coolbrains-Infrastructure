using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Host.AspNetCore.Authentication;
using CoolBrains.Infrastructure.OAuth;
using CoolBrains.Infrastructure.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace OAuthTest
{
    public class AuthenticationService
    {
        private readonly IOauthAccessTokenGenerator _tokenGenerator;
        private readonly TokenConfig _tokenConfig;
        public AuthenticationService(IOauthAccessTokenGenerator tokenGenerator, IOptions<TokenConfig> tokenConfig)
        {
            _tokenGenerator = tokenGenerator;
            _tokenConfig = tokenConfig.Value;
        }
        public TokenInfo GetAnonymousToken(HttpContext context)
        {
            var userContext = new UserContext();
            userContext.Audiences = "security.coolbrains.co";
            userContext.Roles = new[] { "anonymous" };
            //TODO
            userContext.TokenIssuer = _tokenConfig.TokenIssuers.First();
            context.SetClientIdAndTenantIdToUserContext(userContext);
            return userContext.TenantId == Guid.Empty ? null : _tokenGenerator.GenerateAccessToken(userContext);
        }


    }
}
