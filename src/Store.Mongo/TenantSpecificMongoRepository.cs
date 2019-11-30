using System;
using System.Collections.Generic;
using System.Text;

namespace CoolBrains.Infrastructure.Store.Mongo
{
    public class TenantSpecificMongoRepository: MongoRepository
    {
        public TenantSpecificMongoRepository(TenantSpecificDbConnectionBuilder dbConnectionBuilder) : base(dbConnectionBuilder)
        {
        }
    }
}
