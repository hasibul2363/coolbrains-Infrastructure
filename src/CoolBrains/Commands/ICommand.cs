using System;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Commands
{
    public interface ICommand
    {
        UserContext UserContext { get;}
        DateTime TimeStamp { get; set; }
        bool PublishEvent { get; set; }
        void SetUserContext(UserContext userContext);

    }
}
