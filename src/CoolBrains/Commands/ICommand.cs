using System;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Commands
{
    public interface ICommand: ISecurityInfo
    {
        DateTime TimeStamp { get; set; }
        bool PublishEvent { get; set; }
    }
}
