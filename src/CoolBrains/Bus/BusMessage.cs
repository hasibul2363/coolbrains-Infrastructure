using System;
using System.Collections.Generic;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Bus
{
    public abstract class BusMessage : IBusMessage
    {
        public DateTime? ScheduledEnqueueTimeUtc { get; set; }
        public IDictionary<string, object> Properties { get; set; }
        public UserContext UserContext { get; private set; }
        public void SetUserContext(UserContext userContext)
        {
            UserContext = userContext;
        }
    }
}
