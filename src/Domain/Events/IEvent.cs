using System;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Domain.Events
{
    public interface IEvent
    {
        UserContext UserContext { get; }
        Type Source { get; set; }
        DateTime TimeStamp { get; set; }
        void SetUserContext(UserContext userContext);
    }
}
