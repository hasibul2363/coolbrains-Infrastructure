using System;
using System.Collections.Generic;
using System.Text;

namespace CoolBrains.Infrastructure.Session
{
    public interface ISecurityInfo
    {
        UserContext UserContext { get; }
        void SetUserContext(UserContext userContext);
    }
}
