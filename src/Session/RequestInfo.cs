using System;
using System.Collections.Generic;
using System.Text;

namespace CoolBrains.Infrastructure.Session
{
    public class RequestInfo
    {
        public RequestInfo()
        {
            this.Headers = new List<KeyValuePair<string, IEnumerable<string>>>();
        }


        public string HostName { get; set; }
        /// <summary>
        /// This is token
        /// </summary>
        public string AuthorizationParameter { get; set; }
        /// <summary>
        /// This is bearer
        /// </summary>
        public string AuthorizationScheme { get; set; }
        public IEnumerable<KeyValuePair<string, IEnumerable<string>>> Headers { get; set; }
        public Uri RequestUri { get; set; }
    }
}
