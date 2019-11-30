using System;

namespace CoolBrains.Infrastructure.Domain.Events
{
    public interface IEvent
    {
        Guid UserId { get; set; }
        Type Source { get; set; }
        DateTime TimeStamp { get; set; }
    }
}
