using System;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Events
{
    public interface IEvent: ISecurityInfo
    {
        Type Source { get; set; }
        DateTime TimeStamp { get; set; }
    }
}
