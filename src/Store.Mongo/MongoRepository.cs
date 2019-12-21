using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Session;
using CoolBrains.Infrastructure.Store.Abstraction;
using MongoDB.Driver;

namespace CoolBrains.Infrastructure.Store.Mongo
{
    public abstract class MongoRepository : IRepository
    {
        private string _databaseName;
        public IMongoDatabase DataContext { get; set; }
        private IDbConnectionBuilder _dbConnectionBuilder;

        protected MongoRepository(IDbConnectionBuilder dbConnectionBuilder)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
        }

        public void Initialize()
        {
            if (!WillReInitializeDataContext()) return;
            var dbConnectionDetail = _dbConnectionBuilder.Build();
            DataContext = new MongoClient(dbConnectionDetail.ConnectionString).GetDatabase(dbConnectionDetail.DatabaseName);
            _databaseName = dbConnectionDetail.DatabaseName;
        }

        public IQueryable<T> GetItems<T>(Expression<Func<T, bool>> filter, string collectionName = "")
        {
            Initialize();
            var query = DataContext.GetCollection<T>(GetCollectionName<T>()).AsQueryable();
            return filter != null ? query.Where(filter) : query;
        }

        public void Save<T>(T item, string collectionName = "")
        {
            Initialize();
            DataContext.GetCollection<T>(GetCollectionName<T>(collectionName)).InsertOne(item);
        }

        public void Save<T>(List<T> items, string collectionName = "")
        {
            Initialize();
            DataContext.GetCollection<T>(GetCollectionName<T>(collectionName)).InsertMany(items);
        }
        public Task SaveAsync<T>(List<T> items, string collectionName = "")
        {
            Initialize();
            return DataContext.GetCollection<T>(GetCollectionName<T>(collectionName)).InsertManyAsync(items);
        }

        public Task SaveAsync<T>(T item, string collectionName = "")
        {
            Initialize();
            return DataContext.GetCollection<T>(GetCollectionName<T>(collectionName)).InsertOneAsync(item);
        }

        public T GetItem<T>(Expression<Func<T, bool>> filter, string collectionName = "")
        {
            Initialize();
            return DataContext.GetCollection<T>(GetCollectionName<T>(collectionName)).AsQueryable()
                .FirstOrDefault(filter);
        }

        public void Update<T>(Expression<Func<T, bool>> filter, T data, string collectionName = "")
        {
            Initialize();
            DataContext.GetCollection<T>(GetCollectionName<T>(collectionName)).ReplaceOne(filter, data);
        }

        public void UpdateMany<T>(Expression<Func<T, bool>> filter, object data, string collectionName = "")
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(Expression<Func<T, bool>> filter, string collectionName = "")
        {
            Initialize();
            DataContext.GetCollection<T>(GetCollectionName<T>(collectionName)).DeleteMany(filter);
        }


        #region Private Methods

        private bool IsSecurityContextChanged() => _databaseName != _dbConnectionBuilder.Build().DatabaseName;
        private bool WillReInitializeDataContext() => string.IsNullOrEmpty(_databaseName) ||
                                                      _databaseName == Guid.Empty.ToString() ||
                                                      IsSecurityContextChanged();

        private string GetCollectionName<T>(string collectionName = "") =>
            string.IsNullOrEmpty(collectionName) ? $"{typeof(T).Name}s" : collectionName;

        #endregion
    }
}
