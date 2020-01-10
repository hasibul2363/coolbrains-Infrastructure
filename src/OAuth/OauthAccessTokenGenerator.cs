using System;
using System.Collections.Generic;
using System.Text;
using CoolBrains.Infrastructure.Security;
using CoolBrains.Infrastructure.Session;
using Jose;
using Microsoft.Extensions.Options;

namespace CoolBrains.Infrastructure.OAuth
{
    public class OauthAccessTokenGenerator : IOauthAccessTokenGenerator
    {
        private readonly TokenConfig _tokenConfig;
        public OauthAccessTokenGenerator(IOptions<TokenConfig> tokenConfig)
        {
            _tokenConfig = tokenConfig.Value;
        }
        public TokenInfo GenerateAccessToken(UserContext userContext, int tokenLifeTimeInMinutes = 0)
        {
            var secretKey = Encoding.ASCII.GetBytes(_tokenConfig.TokenSigningKey);

            if (tokenLifeTimeInMinutes <= 0)
                tokenLifeTimeInMinutes = _tokenConfig.TokenLifetimeInMinutes;

            if (tokenLifeTimeInMinutes == 0)
                tokenLifeTimeInMinutes = 60;

            var issued = DateTime.UtcNow;
            var expire = DateTime.UtcNow.AddMinutes(tokenLifeTimeInMinutes);

            var payload = new Dictionary<string, object>()
            {
                {ClaimTypes.TokenIssuer, userContext.TokenIssuer},
                {ClaimTypes.ClientId, userContext.ClientId},
                {ClaimTypes.Audience, userContext.Audiences},
                {"iat", issued},
                {"exp", expire},
                {ClaimTypes.TenantId, userContext.TenantId.ToString().ToUpper()},
                {ClaimTypes.UserId, userContext.UserId},
                {ClaimTypes.Role, userContext.Roles}
            };

            string token = JWT.Encode(payload, secretKey, JwsAlgorithm.HS256);
            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = userContext.UserId,
                DateCreated = issued,
                ExpiresIn = DateTime.UtcNow.AddHours(_tokenConfig.RefreshTokenLifeTimeInHours)
            };


            var tokenInfo = new TokenInfo
            {
                Token = token
            };

            tokenInfo.SetRefreshToken(refreshToken);
            return tokenInfo;
        }

        
    }
}
