using System;
using System.Collections.Generic;
using System.Text;

namespace CoolBrains.Infrastructure.Domain
{
    public interface IEvent
    {
        Guid UserId { get; set; }
        Type Source { get; set; }
        DateTime TimeStamp { get; set; }
    }
}
