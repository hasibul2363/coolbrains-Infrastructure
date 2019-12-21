using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoolBrains.Infrastructure.Store.Abstraction
{
    public interface IRepository
    {
        IQueryable<T> GetItems<T>(Expression<Func<T, bool>> filter, string collectionName = "");
        void Save<T>(T item, string collectionName = "");
        void Save<T>(List<T> items, string collectionName = "");
        Task SaveAsync<T>(List<T> items, string collectionName = "");
        Task SaveAsync<T>(T item, string collectionName = "");
        T GetItem<T>(Expression<Func<T, bool>> filter, string collectionName = "");
        void Update<T>(Expression<Func<T, bool>> filter, T data, string collectionName = "");
        void UpdateMany<T>(Expression<Func<T, bool>> filter, object data, string collectionName = "");
        void Delete<T>(Expression<Func<T, bool>> filter, string collectionName = "");
    }
}
