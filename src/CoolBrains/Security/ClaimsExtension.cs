using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Security
{
    public static class ClaimsExtension
    {
        public static UserContext GetAuthData(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
                return new UserContext();

            var claims = claimsPrincipal.Claims.ToList();
            var tenantId = claims.GetClaimValue(ClaimTypes.TenantId);
            var userId = claims.GetClaimValue(ClaimTypes.UserId);
            if (string.IsNullOrEmpty(tenantId) || string.IsNullOrEmpty(userId))
                return new UserContext();

            var userContext = new UserContext(Guid.Parse(userId), Guid.Parse(tenantId))
            {
                ClientId = Guid.Parse(claims.GetClaimValue(ClaimTypes.ClientId)),
                //TODO
                Audiences = "*"//claims.GetClaimValues(ClaimTypes.Audience)?.ToArray()
            };

            var roles = claims.GetClaimValues(System.Security.Claims.ClaimTypes.Role);
            if (roles != null)
                userContext.Roles = roles.ToArray();

            return userContext;
        }
        public static ClaimsPrincipal GetPrincipal(UserContext userContext)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.TenantId, userContext.TenantId.ToString()),
                new Claim(ClaimTypes.UserId, userContext.UserId.ToString()),
                new Claim(ClaimTypes.TokenIssuer, userContext.TokenIssuer),
            };

            if (userContext.Roles != null && userContext.Roles.Length > 0)
            {
                foreach (var role in userContext.Roles)
                {
                    claims.Add(new Claim(System.Security.Claims.ClaimTypes.Role, role));
                }
            }

            if (userContext.Audiences != null && userContext.Audiences.Length > 0)
            {
                //foreach (var audience in userContext.Audiences)
                //{
                //    claims.Add(new Claim(ClaimTypes.Audience, audience));
                //}

                claims.Add(new Claim(ClaimTypes.Audience, userContext.Audiences));
            }

            var identity = new ClaimsIdentity(claims);
            return new ClaimsPrincipal(identity);
        }
        private static string GetClaimValue(this IEnumerable<Claim> claims, string typeKey, string defaultValue = null)
        {
            if (claims == null)
                return defaultValue;

            var targetClaim = claims.FirstOrDefault(c => c.Type == typeKey);
            return targetClaim == null ? defaultValue : targetClaim.Value;
        }
        private static List<string> GetClaimValues(this IEnumerable<Claim> claims, string typeKey, string defaultValue = null)
        {
            if (claims == null)
                return null;
            var targetClaims = claims.Where(c => c.Type == typeKey).ToList();
            return targetClaims.Count <= 0 ? null : targetClaims.Select(p => p.Value).ToList();
        }
    }
}
