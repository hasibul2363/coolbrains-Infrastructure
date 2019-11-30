using System;
using System.Collections.Generic;
using System.Text;

namespace CoolBrains.Infrastructure.Store.Mongo
{
    public class ClassicMongoRepository : MongoRepository
    {
        public ClassicMongoRepository(ClassicDbConnectionBuilder dbConnectionBuilder) : base(dbConnectionBuilder)
        {
        }
    }
}
