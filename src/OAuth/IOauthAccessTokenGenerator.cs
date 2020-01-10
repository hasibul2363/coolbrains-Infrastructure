using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.OAuth
{
    public interface IOauthAccessTokenGenerator
    {
        TokenInfo GenerateAccessToken(UserContext userContext, int tokenLifeTimeInMinutes = 0);
    }
}
