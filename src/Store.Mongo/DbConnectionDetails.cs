using System;
using System.Collections.Generic;
using System.Text;

namespace CoolBrains.Infrastructure.Store.Mongo
{
    public class DbConnectionDetails
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string Suffix { get; set; }

    }
}
