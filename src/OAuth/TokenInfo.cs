using System;
using System.Collections.Generic;
using System.Text;
using CoolBrains.Infrastructure.Util;
using Newtonsoft.Json;

namespace CoolBrains.Infrastructure.OAuth
{
    public class TokenInfo
    {
        public string Token { get; set; }
        public string RefreshToken
        {
            get
            {
                if (_refreshToken != null)
                    return JsonConvert.SerializeObject(_refreshToken).Base64Encode();
                return string.Empty;
            }
        }

        private RefreshToken _refreshToken;
        public void SetRefreshToken(RefreshToken refreshToken) => _refreshToken = refreshToken;
        public RefreshToken GetRefreshToken() => _refreshToken;
    }
}
