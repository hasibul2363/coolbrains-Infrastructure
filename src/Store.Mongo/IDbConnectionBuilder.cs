using System;
using System.Collections.Generic;
using System.Text;

namespace CoolBrains.Infrastructure.Store.Mongo
{
    public interface IDbConnectionBuilder
    {
        DbConnectionDetails Build();
    }
}
