using System;
using System.Collections.Generic;
using System.Text;

namespace CoolBrains.Infrastructure.OAuth
{
    public class TokenConfig
    {
        public string TokenSigningKey { get; set; }
        public int TokenLifetimeInMinutes { get; set; }
        public int RefreshTokenLifeTimeInHours { get; set; }
        public List<string> TokenIssuers { get; set; }



    }
}
