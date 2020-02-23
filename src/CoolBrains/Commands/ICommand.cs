using System;
using CoolBrains.Infrastructure.Bus;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Commands
{
    public interface ICommand : IMessage
    {
        bool PublishEvent { get; set; }
    }
}
