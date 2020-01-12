using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CoolBrains.Infrastructure.Session;
using Jose;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ClaimTypes = CoolBrains.Infrastructure.Security.ClaimTypes;

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

            var issued = DateTime.Now;
            var expire = DateTime.Now.AddMinutes(tokenLifeTimeInMinutes);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.ClientId, userContext.ClientId.ToString()),
                new Claim(ClaimTypes.TenantId, userContext.TenantId.ToString()),
                new Claim(ClaimTypes.UserId, userContext.UserId.ToString()),
                new Claim(ClaimTypes.Audience, "*")
            };

            //TODO
            claims.Add(new Claim(ClaimTypes.TokenIssuer, _tokenConfig.TokenIssuers.First()));
            claims.Add(new Claim(ClaimTypes.Role, userContext.Roles.First()));
            

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(_tokenConfig.TokenLifetimeInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = userContext.UserId,
                DateCreated = issued,
                ExpiresIn = DateTime.UtcNow.AddHours(_tokenConfig.RefreshTokenLifeTimeInHours)
            };


            var tokenInfo = new TokenInfo
            {
                Token = tokenString
            };

            tokenInfo.SetRefreshToken(refreshToken);
            return tokenInfo;
        }

        private long ToUnixTime(DateTime dateTime)
        {
            return (int)(dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
