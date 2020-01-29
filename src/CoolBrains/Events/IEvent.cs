using System;
using CoolBrains.Infrastructure.Bus;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Events
{
    public interface IEvent: IMessage
    {
        string Source { get; set; }
    }
}
