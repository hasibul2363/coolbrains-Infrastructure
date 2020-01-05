using System;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Events
{
    public interface IEvent: ISecurityInfo
    {
        string Source { get; set; }
        DateTime TimeStamp { get; set; }
    }
}
