using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoolBrains.Infrastructure.Store.Mongo
{
    public class ClassicDbConnectionBuilder : IDbConnectionBuilder
    {
        private readonly DbConnectionDetails _dbConnection;
        public ClassicDbConnectionBuilder(IOptions<DbConnectionDetails> dbConnection)
        {
            _dbConnection = dbConnection.Value;
        }
        public DbConnectionDetails Build()
        {
            return _dbConnection;
        }
    }
}
