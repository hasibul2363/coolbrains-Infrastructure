using System;
using System.Collections.Generic;
using System.Text;
using CoolBrains.Infrastructure.Session;
using Microsoft.Extensions.Options;

namespace CoolBrains.Infrastructure.Store.Mongo
{
    public class TenantSpecificDbConnectionBuilder : IDbConnectionBuilder
    {
        private readonly UserContext _userContext;
        private readonly DbConnectionDetails _dbConnection;
        public TenantSpecificDbConnectionBuilder(UserContext userContext, IOptions<DbConnectionDetails> dbConnection)
        {
            _userContext = userContext;
            _dbConnection = dbConnection.Value;
        }


        public DbConnectionDetails Build()
        {
            return new DbConnectionDetails
            {
                ConnectionString = _dbConnection.ConnectionString,
                DatabaseName = string.IsNullOrEmpty(_dbConnection.Suffix)? _userContext.TenantId.ToString() : $"{_userContext.TenantId.ToString()}_{_dbConnection.Suffix}" };
        }

    }
}
