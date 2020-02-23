using System;
using System.Collections.Generic;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Bus
{
    public abstract class Message : IMessage
    {
        public DateTime? ScheduledEnqueueTimeUtc { get; set; }
        public IDictionary<string, object> Properties { get; set; }
        public UserContext UserContext { get; private set; }
        public Guid CorrelationId { get; set; }
        public void SetUserContext(UserContext userContext) => UserContext = userContext;
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}
