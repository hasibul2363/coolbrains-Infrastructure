using System;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Bus
{
        
    public interface IMessage : ISecurityInfo
    {
        Guid CorrelationId { get; set; }
        DateTime TimeStamp { get; set; }
    }
}
