using System;
using System.Collections.Generic;
using System.Text;
using CoolBrains.Infrastructure.Session;

namespace Commands
{
    public interface ICommand
    {
        UserContext UserContext { get;}
        DateTime TimeStamp { get; set; }
        bool PublishEvent { get; set; }
        void SetUserContext(UserContext userContext);

    }
}
